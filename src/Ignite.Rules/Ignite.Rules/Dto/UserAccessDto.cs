using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class UseraccessDto
    {
        [JsonProperty("NonRegistered")]
        public NonregisteredDto NonRegistered { get; set; }
        [JsonProperty("ConferenceAttendee")]
        public ConferenceattendeeDto ConferenceAttendee { get; set; }
        [JsonProperty("Staff")]
        public StaffDto Staff { get; set; }
        [JsonProperty("BoothStaff")]
        public BoothstaffDto BoothStaff { get; set; }
        [JsonProperty("Speaker")]
        public SpeakerDto Speaker { get; set; }
        [JsonProperty("Press")]
        public PressDto Press { get; set; }
        [JsonProperty("Crew")]
        public CrewDto Crew { get; set; }
        [JsonProperty("ExpoOnly")]
        public ExpoonlyDto ExpoOnly { get; set; }
        [JsonProperty("DayPass")]
        public DaypassDto DayPass { get; set; }
    }
}