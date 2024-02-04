using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Schedules.Assessment
{
    public class RiskSurveyDetail : BaseEntity
    {
        public Guid SurveyId { get; set; }
        public RiskSurvey Survey { get; set; }

        public Guid DetailId { get; set; }
        public RiskDetail Detail { get; set; }


        public string Response { get; set; }
        public string Notes { get; set; }



    }
}
