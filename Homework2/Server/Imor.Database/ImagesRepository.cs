using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imor.Business;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Writing;
using StringWriter = System.IO.StringWriter;

namespace Imor.Database
{
    public class ImagesRepository
    {
        private IGraph graph;

        private readonly TagsRepository tagsRepository;

        public ImagesRepository()
        {
            graph = DatabaseInitializer.Initialize();
            tagsRepository = new TagsRepository();
        }

        public IEnumerable<ImorImage> GetImages()
        {
            var results = new List<ImorImage>();

            var imageNode = graph.GetUriNode(new Uri(ImorEnum.Image));

            var images = graph.GetTriplesWithObject(imageNode);

            foreach (var image in images)
            {
                var imageProperties = graph.GetTriplesWithSubject(image.Subject);

                var imorImage = this.MapImage(image.Subject.ToString(), imageProperties);

                imorImage.Tags = this.tagsRepository.GetTagsForImage(imorImage.Uri);

                results.Add(imorImage);
            }

            return results;
        }

        public ImorImage GetImageByUri(string uri)
        {
            var imageNode = this.graph.GetUriNode(new Uri(uri));

            var properties = this.graph.GetTriplesWithSubject(imageNode);

            var image = this.MapImage(uri, properties);

            image.Tags = this.tagsRepository.GetTagsForImage(uri);

            return image;
        }

        public IEnumerable<ImorImage> GetSimilarImages(string imageUri, int number)
        {
            var similarImages = new List<ImorImage>();

            var image = this.GetImageByUri(imageUri);

            foreach (var tag in image.Tags)
            {
                var similarTags = this.tagsRepository.GetSimilarTags(tag.Uri);


                foreach (var similarTag in similarTags)
                {
                    var images = this.SearchImagesByTag(similarTag.Uri);

                    similarImages.AddRange(images);
                }
            }

            return similarImages;
        }

        public IEnumerable<ImorImage> SearchImagesByTag(string tagUri, int number = 0)
        {
            var results = new List<ImorImage>();

            var tagNode = graph.GetUriNode(new Uri(tagUri));

            var hasNode = graph.GetUriNode(new Uri(ImorEnum.HasA));

            var images = graph.GetTriplesWithPredicateObject(hasNode, tagNode);

            if (number > 0)
            {
                images = images.Take(number);
            }

            foreach (var image in images)
            {
                var imageProperties = graph.GetTriplesWithSubject(image.Subject);

                var imorImage = this.MapImage(image.Subject.ToString(), imageProperties);

                results.Add(imorImage);
            }

            return results;
        }

        public IEnumerable<ImorImage> SearchImagesByTags(IEnumerable<string> tagsUriList)
        {
            var results = new List<ImorImage>();

            ////var results1 = graph.Triples.ObjectNodes.Select(x => x.AsValuedNode().AsString()).Intersect(tagsUriList)
            ////    .ToList();

            return results;
        }

        public void InsertImage(ImorImage image)
        {
            graph = DatabaseInitializer.Initialize();

            var node = graph.CreateUriNode(new Uri(image.Uri));

            var typeNode = graph.GetUriNode(new Uri(ImorEnum.RdfType));

            var imageNode = graph.GetUriNode(new Uri(ImorEnum.Image));

            var t = new Triple(node, typeNode, imageNode);

            if (!string.IsNullOrEmpty(image.Description))
            {
                var descriptionNode = graph.GetUriNode(new Uri(ImorEnum.Description));

                var descriptionTriple = new Triple(node, descriptionNode, graph.CreateLiteralNode(image.Description));

                graph.Assert(descriptionTriple);
            }

            if (!string.IsNullOrEmpty(image.Content))
            {
                var contentNode = graph.GetUriNode(new Uri(ImorEnum.Content));

                var contenTriple = new Triple(node, contentNode, graph.CreateLiteralNode(image.Content));

                graph.Assert(contenTriple);
            }

            if (image.Tags.Any())
            {
                foreach (var tag in image.Tags)
                {
                    var tagNode = graph.GetUriNode(new Uri(tag.Uri));

                    if (tagNode != null)
                    {
                        var hasNode = graph.GetUriNode(new Uri(ImorEnum.HasA));

                        var tagTriple = new Triple(node, hasNode, tagNode);

                        graph.Assert(tagTriple);
                    }
                }
            }

            graph.Assert(t);

            graph.SaveToFile(DatabaseInitializer.ontology);
        }

        private ImorImage MapImage(string imageUri, IEnumerable<Triple> triples)
        {
            var imorImage = new ImorImage
            {
                Uri = imageUri
            };

            foreach (var imageProperty in triples)
            {
                var propertyName = ImorImage.RdfPropertiesDictionary.SingleOrDefault(x => x.Key == imageProperty.Predicate.ToString());

                var properties = imorImage.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name == propertyName.Value)
                    {
                        property.SetValue(imorImage, imageProperty.Object.AsValuedNode().AsString());
                    }
                }
            }

            return imorImage;
        }
    }
}