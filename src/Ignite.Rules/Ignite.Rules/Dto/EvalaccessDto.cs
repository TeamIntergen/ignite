using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class EvalaccessDto
    {
        [JsonProperty("Identifier")]
        public string Identifier { get; set; }
        [JsonProperty("SessionTypes")]
        public string[] SessionTypes { get; set; }
        [JsonProperty("Mobile")]
        public bool Mobile { get; set; }
        [JsonProperty("Website")]
        public bool WebSite { get; set; }
    }
}