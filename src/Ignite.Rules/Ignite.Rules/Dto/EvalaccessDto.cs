using Newtonsoft.Json;

namespace Ignite.Rules.Dto
{
    public class EvalaccessDto
    {
        [JsonProperty("PreEventSurvey")]
        public SurveyAccessDto PreEventSurvey { get; set; }
        [JsonProperty("PostEventSurvey")]
        public SurveyAccessDto PostEventSurvey { get; set; }
        [JsonProperty("PreDaySessionEvaluations")]
        public SurveyAccessDto PreDaySessionEvaluations { get; set; }
        [JsonProperty("RegularSessionEvaluations")]
        public SurveyAccessDto RegularSessionEvaluations { get; set; }
        [JsonProperty("KeynoteDayEvaluation")]
        public SurveyAccessDto KeynoteDayEvaluation { get; set; }
        [JsonProperty("IDL_Evaluations")]
        public SurveyAccessDto IDL_Evaluations { get; set; }
        [JsonProperty("TEFPreSurvey")]
        public SurveyAccessDto TEFPreSurvey { get; set; }
        [JsonProperty("PlusPass")]
        public SurveyAccessDto PlusPass { get; set; }
    }
}