using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Models
{
    public class ExchangeRatesData
    {
        [JsonPropertyName("data")]
        public ExchangeRates exchangeRates { get; set; }
    }
    public class ExchangeRates
    {
        [JsonPropertyName("currency")]
        public string crypto { get; set; }
        [JsonPropertyName("rates")]
        public Dictionary<string, string> rates { get; set; }
        //[JsonPropertyName("rates")]
        //public List<Rate> rates { get; set; }
    }
}
