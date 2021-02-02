using Newtonsoft.Json;

namespace TechnineUzduotis.Models
{
    public class Country
    {
        [JsonProperty("country")]
        public string Name { get; set; }
        [JsonProperty("standard_rate")]
        public double Vat { get; set; }
    }
}
