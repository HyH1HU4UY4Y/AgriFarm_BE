namespace Service.FarmCultivation.DTOs
{
    public class SeasonDetailResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }
    }
}
