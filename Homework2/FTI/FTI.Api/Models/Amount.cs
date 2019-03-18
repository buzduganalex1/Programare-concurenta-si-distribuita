using FTI.Api.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FTI.Api.Models
{
    public class Amount
    {
        public Amount(CurrencyEnum currency, float value)
        {
            this.Currency = currency;
            this.Value = value;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyEnum Currency { get; private set; }

        public float Value { get; private set; }

        public new string ToString => $"{this.Value} {this.Currency}";
    }
}