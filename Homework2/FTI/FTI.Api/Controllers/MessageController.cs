using System;
using FTI.Api.Models;
using FTI.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
        public IActionResult Post([FromBody] Message message)
        {
            var receipt = new Receipt();

            receipt.AddCustomerNumber("123");
            receipt.AddItem(new Item("Milk", new Amount(CurrencyEnum.EUR, 10.0f)));
            receipt.AddItem(new Item("Egs", new Amount(CurrencyEnum.EUR, 5.0f)));
            receipt.AddItem(new Item("Honey", new Amount(CurrencyEnum.EUR, 2.0f)));

            Console.WriteLine(message.Payload);

            if(message.Type== "Printable Text"){
                message.Payload = receipt.ToJson();

                return Ok(message);
            }

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