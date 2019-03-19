using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace FTI.ConversionFunction.Models
{
    public class Receipt
    {
        public Receipt()
        {
            this.Items = new List<Item>();
        }

        public string CustomerNumber { get; private set; }

        public IList<Item> Items { get; }

        public Amount Total
        {
            get { return new Amount(CurrencyEnum.EUR, Items.Sum(x => x.Price.Value)); }
        }

        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }

        public void AddCustomerNumber(string customerNumber)
        {
            this.CustomerNumber = customerNumber;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public string ToPlainText()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CustomerNumber: " + this.CustomerNumber);
            stringBuilder.AppendLine("Items: ");
            foreach (var item in this.Items)
            {
                stringBuilder.AppendLine($"{item.Description} {item.Price.ToString}");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Total: " + this.Total.ToString);

            return stringBuilder.ToString();
        }
        
        public string ToXml()
        {
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(this.ToJson())))
            {
                var quotas = new XmlDictionaryReaderQuotas();
                return XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(stream, quotas)).ToString();
            }
        }
    }
}