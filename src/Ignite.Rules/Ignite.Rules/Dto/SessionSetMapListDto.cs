using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class SessionSetMapListDto
    {
        [JsonProperty("SessionSetMap")]
        public SessionsetMapDto[] SessionSetMap { get; set; }
    }
}