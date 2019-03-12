using System;
using System.Collections.Generic;
using System.Linq;
using Imor.Business;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace Imor.Database
{
    public class TagsRepository
    {
        private readonly IGraph graph;

        public TagsRepository()
        {
            this.graph = DatabaseInitializer.Initialize();
        }

        public IEnumerable<ImorTag> GetAllTags()
        {
            var tags = new List<ImorTag>();

            var tagNode = graph.GetUriNode(new Uri(ImorEnum.Tag));

            var tagsTriples = graph.GetTriplesWithObject(tagNode);

            foreach (var tagsTriple in tagsTriples.Where(x => x.Predicate.ToString() == ImorEnum.RdfType))
            {
                var tagProperties = graph.GetTriplesWithSubject(tagsTriple.Subject);

                var tag = this.MapImorTag(tagsTriple.Subject.AsValuedNode().AsString(), tagProperties);

                tags.Add(tag);
            }

            return tags;
        }

        public IEnumerable<ImorTag> GetTagsForImage(string imageUri)
        {
            var tags = new List<ImorTag>();

            var imageNode = graph.GetUriNode(new Uri(imageUri));

            var hasA = graph.GetUriNode(new Uri(ImorEnum.HasA));

            var triplesForNode = graph.GetTriplesWithSubjectPredicate(imageNode, hasA);

            foreach (var triple in triplesForNode)
            {
                var tag = new ImorTag
                {
                    Uri = triple.Object.ToString()
                };

                var tagProperties = graph.GetTriplesWithSubject(triple.Object);

                foreach (var property in tagProperties)
                {
                    if (property.Predicate.ToString() == ImorEnum.TagLabel)
                    {
                        tag.Label = property.Object.AsValuedNode().AsString();
                    }

                    if (property.Predicate.ToString() == ImorEnum.Description)
                    {
                        tag.Description = property.Object.AsValuedNode().AsString();
                    }
                }

                tags.Add(tag);
            }
            
            return tags;
        }
        
        public ImorTag GetTagByUri(string uri)
        {
            var imorUri = new Uri(ImorEnum.GetUri(uri));

            var tagNode = graph.GetUriNode(imorUri);

            if (tagNode == null)
            {
                return null;
            }

            var properties = graph.GetTriplesWithSubject(tagNode);

            var tag = this.MapImorTag(imorUri.ToString(), properties);
            
            return tag;
        }

        public ImorTag GetTagByUri(Uri uri)
        {
            var tagNode = graph.GetUriNode(uri);

            if (tagNode == null)
            {
                return null;
            }

            var properties = graph.GetTriplesWithSubject(tagNode);

            var tag = this.MapImorTag(uri.ToString(), properties);

            return tag;
        }

        public void InsertImorTag(ImorTag tag)
        {
            var node = graph.CreateUriNode(new Uri(tag.Uri));

            var typeNode = graph.GetUriNode(new Uri(ImorEnum.RdfType));

            var tagNode = graph.GetUriNode(new Uri(ImorEnum.Tag));

            var t = new Triple(node, typeNode, tagNode);

            if (!string.IsNullOrEmpty(tag.Description))
            {
                var descriptionNode = graph.GetUriNode(new Uri(ImorEnum.Description));

                var descriptionTriple = new Triple(node, descriptionNode, graph.CreateLiteralNode(tag.Description));

                graph.Assert(descriptionTriple);
            }

            if (!string.IsNullOrEmpty(tag.Label))
            {
                var label = graph.GetUriNode(new Uri(ImorEnum.TagLabel));

                var contenTriple = new Triple(node, label, graph.CreateLiteralNode(tag.Label));

                graph.Assert(contenTriple);
            }
            
            graph.Assert(t);

            try
            {
                graph.SaveToFile(DatabaseInitializer.ontology);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<ImorTag> GetSimilarTags(string uri)
        {
            var tags = new List<ImorTag>();

            var tagNode = graph.GetUriNode(new Uri(uri));

            var isSimilarNode = graph.GetUriNode(new Uri(ImorEnum.IsSimilar));

            var similarNodes = graph.GetTriplesWithSubjectPredicate(tagNode, isSimilarNode);

            foreach (var similarNode in similarNodes)
            {
                var nodeUri = similarNode.Object.AsValuedNode().AsString();

                var properties = graph.GetTriplesWithSubject(new Uri(nodeUri));

                var tag = this.MapImorTag(nodeUri, properties);

                tags.Add(tag);
            }

            return tags;
        }

        private ImorTag MapImorTag(string uri, IEnumerable<Triple> tagProperties)
        {
            var tag = new ImorTag()
            {
                Uri = uri
            };

            foreach (var property in tagProperties)
            {
                if (property.Predicate.ToString() == ImorEnum.TagLabel)
                {
                    tag.Label = property.Object.AsValuedNode().AsString();
                }

                if (property.Predicate.ToString() == ImorEnum.Description)
                {
                    tag.Description = property.Object.AsValuedNode().AsString();
                }
            }

            return tag;
        }
    }
}