namespace Service.Training.DTOs
{
    public class DetailResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExpertResponse Expert { get; set; }
        public ContentResponse Content { get; set; }

    }
}
