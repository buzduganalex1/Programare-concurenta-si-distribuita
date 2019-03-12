using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FTI.Api.Models
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
    }
}