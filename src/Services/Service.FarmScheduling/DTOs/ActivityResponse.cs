namespace Service.FarmScheduling.DTOs
{
    public class ActivityResponse
    {
        public Guid Id { get; set; }
        public SeasonResponse Season { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? StartIn { get; set; }
        public DateTime? EndIn { get; set; }
    }
}
