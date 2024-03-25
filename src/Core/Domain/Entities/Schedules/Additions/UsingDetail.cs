using SharedDomain.Entities.FarmComponents;

namespace SharedDomain.Entities.Schedules.Additions
{
    public class UsingDetail : AdditionOfActivity
    {
        public UsingDetail()
        {
            AdditionType = Defaults.AdditionType.Use;
        }
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
        public double UseValue { get; set; }
        public string Unit { get; set; }

    }
}
