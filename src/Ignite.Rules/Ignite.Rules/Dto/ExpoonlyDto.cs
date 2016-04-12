using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class ExpoonlyDto
    {
        [JsonProperty("ExpoOnly")]
        public ProfileDto ExpoOnly { get; set; }
    }
}