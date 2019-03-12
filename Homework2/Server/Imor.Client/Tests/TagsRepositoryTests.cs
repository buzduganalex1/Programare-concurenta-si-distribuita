using System;
using Imor.Business;
using Imor.Database;
using Newtonsoft.Json;

namespace Imor.Client.Tests
{
    public class TagsRepositoryTests
    {
        public void Run()
        {
            this.GetTagsForImage();

            this.GetTagsTest();
            
        }

        private void GetTagsTest()
        {
            var repository = new TagsRepository();

            var results = repository.GetAllTags();

            foreach (var result in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
        }

        private void GetTagsForImage()
        {
            var repository = new TagsRepository();

            var results = repository.GetTagsForImage("CatImage");

            foreach (var result in results)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
        }
    }
}