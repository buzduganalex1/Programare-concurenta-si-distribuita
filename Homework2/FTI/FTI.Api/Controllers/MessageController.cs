using System;
using FTI.Api.Models;
using FTI.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FTI.Api.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }

        [HttpPost]
        public ActionResult<Message> Post([FromBody] Message message)
        {
            var receipt = JsonConvert.DeserializeObject<Receipt>(message.Payload);
            
            Console.WriteLine(message);

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

            Console.WriteLine(message.Payload);

            return Ok(message);
        }
    }

    public class Message
    {
        public string Id {get;set;}

        public string Type { get; set; }

        public string Payload { get; set; }
    }
}