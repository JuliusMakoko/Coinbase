using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Models
{
    public class Rate
    {
        //[JsonPropertyName("currency")]
        //public string currency { get; set; }
        [JsonPropertyName("value")]
        public string value { get; set; }
    }
}
