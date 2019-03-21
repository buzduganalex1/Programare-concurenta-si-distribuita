using System;
using System.Net.Http;
using System.Net.Http.Headers;
using FTI.Business;
using FTI.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FTI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IPublisher messagePublisher;
        private readonly IHubContext<NotifyHub, ITypedHubClient> messageHubContext;
        
        public ReceiptController(IPublisher messagePublisher, IHubContext<NotifyHub, ITypedHubClient> messageHubContext)
        {
            this.messagePublisher = messagePublisher;
            this.messageHubContext = messageHubContext;
        }
        
        [HttpGet]   
        public IActionResult Get()
        {
            var receipt = new Receipt();

            receipt.AddCustomerNumber("123");
            receipt.AddItem(new Item("Milk", new Amount(CurrencyEnum.EUR, 10.0f)));
            receipt.AddItem(new Item("Egs", new Amount(CurrencyEnum.EUR, 5.0f)));
            receipt.AddItem(new Item("Honey", new Amount(CurrencyEnum.EUR, 2.0f)));
            
            return Ok(receipt.ToJson());
        }
        
        [HttpPost]
        public void Post([FromBody] Receipt receipt)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Json",
                Payload = receipt.ToJson()
            };

            NotifyFinancialSistem(receipt);
            
            messageHubContext.Clients.All.BroadcastMessage(message.Type, message.Payload, message.Id);

            this.messagePublisher.PublishMessage(receipt.ToJson());
        }

        private static void NotifyFinancialSistem(Receipt receipt)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, EnvResources.FinancialServiceUrl);

                    request.Content = new StringContent(receipt.ToJson());
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.SendAsync(request).Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
