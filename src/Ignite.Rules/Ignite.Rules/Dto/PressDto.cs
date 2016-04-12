using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class PressDto
    {
        [JsonProperty("Press")]
        public ProfileDto Press { get; set; }
    }
}