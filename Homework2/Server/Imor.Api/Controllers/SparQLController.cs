using System;
using System.Collections.Generic;
using Imor.Api.Contracts;
using Imor.Database;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Imor.Api.Controllers
{
    [Route("api/sparql")]
    public class SparQLController : Controller
    {
        [HttpGet]
        [EnableCors("AllowSpecificOrigin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public string Post([FromBody]SparQLQueryRequest request)
        {
            try
            {
                var repository = new SparQlRepository();

                var results = repository.Execute(request.Query);

                return results;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
