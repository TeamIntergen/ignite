using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class BoothstaffDto
    {
        [JsonProperty("Sponsor")]
        public ProfileDto Sponsor { get; set; }
        [JsonProperty("Exhibitor")]
        public ProfileDto Exhibitor { get; set; }
    }
}