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
        public IActionResult Post([FromBody] Receipt receipt)
        {
            
            return Ok(receipt);
        }
    }

    public class Message
    {
        public string Type { get; set; }

        public string Payload { get; set; }
    }
}