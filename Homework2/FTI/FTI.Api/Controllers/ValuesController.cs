using System;
using FTI.Api.Models;
using FTI.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FTI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPublisher _publisher;
        private readonly IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public ValuesController(IPublisher publisher, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _publisher = publisher;
            _hubContext = hubContext;
        }
        
        [HttpGet]   
        public IActionResult Get()
        {
            var receipt = new Receipt();

            receipt.AddCustomerNumber("123");
            receipt.AddItem(new Item("Milk", new Amount(CurrencyEnum.EUR, 10.0f)));
            receipt.AddItem(new Item("Egs", new Amount(CurrencyEnum.EUR, 5.0f)));
            receipt.AddItem(new Item("Honey", new Amount(CurrencyEnum.EUR, 2.0f)));

            var message = new Message()
            {
                Payload = receipt.ToJson(),
                Type = "Json",
                Id = Guid.NewGuid().ToString()
            };

            _hubContext.Clients.All.BroadcastMessage(message.Type, message.Payload, message.Id);

            return Ok(receipt.ToJson());
        }
        
        [HttpPost]
        public void Post([FromBody] Receipt receipt)
        {
            Console.WriteLine(receipt.ToJson());

            var message = new Message()
            {
                Payload = receipt.ToJson(),
                Type = "Json",
                Id = Guid.NewGuid().ToString()
            };

            _hubContext.Clients.All.BroadcastMessage(message.Type, message.Payload, message.Id);

            this._publisher.PublishMessage(receipt.ToJson());
        }
    }
}
