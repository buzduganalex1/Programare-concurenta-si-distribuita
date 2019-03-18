using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FIT.ConversionFunctionAnnonymous
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)]
            HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Get request body
            string data = req.Content.ReadAsStringAsync().Result;

            var message = JsonConvert.DeserializeObject<Message>(data);
            
            var receipt = JsonConvert.DeserializeObject<Receipt>(message.Payload);
            
            switch (message.Type)
            {
                case "Json":
                    message.Payload = receipt.ToJson();
                    return req.CreateResponse(HttpStatusCode.OK, message);
                case "Xml":
                    message.Payload = receipt.ToXml();
                    return req.CreateResponse(HttpStatusCode.OK, message);
                case "PlainText":
                    message.Payload = receipt.ToPlainText();
                    return req.CreateResponse(HttpStatusCode.OK, message);
            }

            return req.CreateResponse(HttpStatusCode.OK, message);
        }


        public class Message
        {
            public string Id { get; set; }

            public string Type { get; set; }

            public string Payload { get; set; }
        }
    }
}
