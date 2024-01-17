using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.GAPs
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
