using System;
using System.Collections.Generic;
using Imor.Business;
using Imor.Database;
using Newtonsoft.Json;

namespace Imor.Client.Tests
{
    public class ImageRepositoryTests
    {
        public void Run()
        {
            //this.GetImagesTest();

            //this.InsertImage();

            //this.SearchImagesByTag();

            this.SearchImagesForTags();
        }

        private void GetImagesTest()
        {
            var repository = new ImagesRepository();

            var results = repository.GetImages();
            
            foreach (var result in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
        }

        private void InsertImage()
        {
            var repository = new ImagesRepository();

            repository.InsertImage(new ImorImage()
            {
                Uri = ImorEnum.GetUri("TestImage3"),
                Description = "Test image 3",
                Content = "https://imagesvc.timeincapp.com/v3/mm/image?url=https%3A%2F%2Ffortunedotcom.files.wordpress.com%2F2017%2F01%2Fgettyimages-632438922.jpg&w=800&q=85",
                Tags = new List<ImorTag>
                {
                    new ImorTag
                    {
                        Uri = "http://www.semanticweb.org/ImagesOntology#CatsTag",
                        Description = "This is a cat tag",
                        Label = "Cats"
                    }
                }
            });
        }

        private void SearchImagesByTag()
        {
            var repo = new ImagesRepository();
            var tagRepo = new TagsRepository();
            
            var results = repo.SearchImagesByTag("http://www.semanticweb.org/ImagesOntology#DogTag");

            foreach (var result in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
        }

        private void SearchImagesForTags()
        {
            var repo = new ImagesRepository();

            var tagRepo = new TagsRepository();

            var tags = new List<string>()
            {
                "http://www.semanticweb.org/ImagesOntology#DogTag",
                "http://www.semanticweb.org/ImagesOntology#PetTag",
                "http://www.semanticweb.org/ImagesOntology#CatsTag"

            };

            var results = repo.SearchImagesByTags(tags);

            foreach (var result in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
        }
    }
}