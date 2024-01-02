using SharedDomain.Base;

namespace SharedDomain.Assessment
{
    public class GAPSurveyDetail : BaseEntity
    {
        public Guid SurveyId { get; set; }
        public GAPSurvey Survey { get; set; }

        public Guid ItemId { get; set; }
        public Criteria Item { get; set; }

        public string Response { get; set; }
        public string Notes { get; set; }



    }
}
