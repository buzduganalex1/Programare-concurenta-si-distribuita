using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Imor.Api.Contracts;
using Imor.Business;
using Newtonsoft.Json;

namespace Imor.Client.Tests
{
    public class ApiTests
    {
        public void Run()
        {
            ////GetImagesAsString();
            ////GetTagsForImage();
            CreateImage();
        }

        private static void GetImagesAsString()
        {
            var client = new HttpClient();

            var request = JsonConvert.SerializeObject(new SparQLQueryRequest()
            {
                Query =
                    "prefix imor: <http://www.semanticweb.org/ImagesOntology#>\r\n\r\nselect ?s \r\nwhere { \r\n\t?s a  imor:Image\r\n} \r\n"
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:52676/api/sparql");

            httpRequest.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            httpRequest.Content = new StringContent(request, Encoding.UTF8, "application/json");

            var result = client.SendAsync(httpRequest).Result;

            Console.WriteLine(request);

            Console.WriteLine(result.StatusCode);

            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
        }

        private static void GetTagsForImage()
        {
            var client = new HttpClient();
            
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:52676/api/tags/forImageUri?imageUri=http%3A%2F%2Fwww.semanticweb.org%2FImagesOntology%23CatImage");

            httpRequest.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var result = client.SendAsync(httpRequest).Result;

            Console.WriteLine(result.StatusCode);

            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
        }

        private static void CreateImage()
        {
            var client = new HttpClient();

            var request = JsonConvert.SerializeObject(new CreateImageCommand()
            {
                Uri = ImorEnum.GetUri("TestImage6"),
                Description = "Test 6",
                Content = "https://wallpaperbrowse.com/media/images/3848765-wallpaper-images-download.jpg",
                Tags = new List<string>()
                {
                    "DogTag",
                    "PetTag",
                    "TestTag"
                }
            });

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:52676/api/images/create");

            httpRequest.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            httpRequest.Content = new StringContent(request, Encoding.UTF8, "application/json");

            var result = client.SendAsync(httpRequest).Result;

            Console.WriteLine(request);

            Console.WriteLine(result.StatusCode);

            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
        }
    }
}