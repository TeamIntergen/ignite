using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class CrewDto
    {
        [JsonProperty("Crew")]
        public ProfileDto Crew { get; set; }
    }
}