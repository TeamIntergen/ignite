using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SurveyAccessDto
    {
        [JsonProperty("Email")]
        public bool Email { get; set; }
        [JsonProperty("Mobile")]
        public bool Mobile { get; set; }
        [JsonProperty("WebSite")]
        public bool WebSite { get; set; }
        [JsonProperty("Lab")]
        public bool Lab { get; set; }
    }
}