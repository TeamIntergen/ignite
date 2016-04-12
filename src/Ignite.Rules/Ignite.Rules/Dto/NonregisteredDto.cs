using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class NonregisteredDto
    {
        [JsonProperty("Anonymous")]
        public ProfileDto Anonymous { get; set; }
        [JsonProperty("AuthenticatedNonAttendee")]
        public ProfileDto AuthenticatedNonAttendee { get; set; }
    }
}