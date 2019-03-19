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
            return Ok("Hello");
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
}