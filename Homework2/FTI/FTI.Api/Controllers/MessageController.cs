using System;
using System.Linq;
using System.Net.Http;
using FTI.Business;
using FTI.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FTI.Api.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, EnvResources.FinancialServiceUrl);

                    var result = client.SendAsync(request).Result;

                    var stringResult = result.Content.ReadAsStringAsync().Result;

                    var objectResult = JsonConvert.DeserializeObject<TotalReceiptResponse[]>(stringResult);

                    return Ok(objectResult.First());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult<Message> ConvertMessage([FromBody] Message message)
        {
            var receipt = JsonConvert.DeserializeObject<Receipt>(message.Payload);

            switch (message.Type)
            {
                case "Json":
                    message.Payload = receipt.ToJson();
                    return Ok(message);
                case "Xml":
                    message.Payload = receipt.ToXml();
                    return Ok(message);
                case "PlainText":
                    message.Payload = receipt.ToPlainText();
                    return Ok(message);
            }

            return Ok(message);
        }
    }

    class TotalReceiptResponse
    {
        public string key { get; set; }

        public string value { get; set; }
    }
}