using System;
using System.Collections.Generic;
using Imor.Api.Contracts;
using Imor.Business;
using Imor.Database;
using Microsoft.AspNetCore.Mvc;

namespace Imor.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/tags")]
    public class TagsController : Controller
    {
        [HttpGet("all")]
        public IEnumerable<ImorTag> Get()
        {
            return new List<ImorTag>(){
                new ImorTag(){
                    Uri = "test",
                    Label = "hello",
                    Description ="test"
                }
            };

            try
            {
                var repo = new TagsRepository();

                return repo.GetAllTags();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpGet("forImageUri")]
        public IEnumerable<ImorTag> GetTagsForImage(string imageUri)
        {
            var repo = new TagsRepository();

            return repo.GetTagsForImage(imageUri);
        }

        [HttpGet("byUri")]
        public ImorTag GetTagByUri(string tagUri)
        {
            var repo = new TagsRepository();

            return repo.GetTagByUri(new Uri(tagUri));
        }

        [HttpGet("similar")]
        public IEnumerable<ImorTag> GetSimilarTags(string tagUri)
        {
            var repo = new TagsRepository();

            return repo.GetSimilarTags(tagUri);
        }

        [HttpPost]
        [Route("create")]
        public void Post([FromBody]CreateTagCommand request)
        {
            var repository = new TagsRepository();

            repository.InsertImorTag(new ImorTag
            {
                Uri = request.Uri,
                Description = request.Description,
                Label = request.Label
            });
        }
    }
}