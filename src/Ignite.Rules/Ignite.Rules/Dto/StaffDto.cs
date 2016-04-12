using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class StaffDto
    {
        [JsonProperty("ExternalCustomerAndPartner")]
        public ProfileDto ExternalCustomerAndPartner { get; set; }

        [JsonProperty("Microsoft")]
        public ProfileDto Microsoft { get; set; }
    }
}