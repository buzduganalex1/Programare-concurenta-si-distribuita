using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FTI.ConversionFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FTI.ConversionFunction
{
    public static class ConversionFunction
    {
        [FunctionName("Function2")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)]
            HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var data = req.Content.ReadAsStringAsync().Result;
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
    }
}
