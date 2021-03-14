using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonLib.Produce
{
    public class ProduceDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nm")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        public decimal Price  { get; set; }
        [JsonPropertyName("s")]
        public int Stock { get; set; }
    }
}
