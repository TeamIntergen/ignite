using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class DaypassDto
    {
        [JsonProperty("DayPassAttendee")]
        public ProfileDto DayPassAttendee { get; set; }
    }
}