using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedDomain.Entities.Schedules
{
    public class Activity : BaseEntity, IMultiSite
    {
        public Guid SeasonId { get; set; }
        public CultivationSeason Season { get; set; }

        public Guid? LocationId { get; set; }
        public FarmSoil Location { get; set; }

        public Guid SiteId { get; set; }
        public Site Site { get; set; }


        public string Title { get; set; }
        public bool IsCompletable { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        public ICollection<AdditionOfActivity> Addtions { get; set; }
        public ICollection<ActivityParticipant> Participants { get; set; }

        public DateTime? StartIn { get; set; }
        public DateTime? EndIn { get; set; }

        [MaxLength(int.MinValue)]
        public string? Resources { get; set; }

        private string _noteStr;
        [MaxLength(int.MaxValue)]
        public string? Note
        {
            get => _noteStr;
            set
            {
                _noteStr = value;

            }
        }

        [NotMapped]
        public List<NoteItem> Notes
        {
            get => !string.IsNullOrWhiteSpace(_noteStr) ?
                JsonConvert.DeserializeObject<List<NoteItem>>(_noteStr)!
                : new();
            set => _noteStr = JsonConvert.SerializeObject(value);
        }

    }

    public class NoteItem
    {
        public string? Name { get; set; }
        public string? Value { get; set; } = "";
    }
}
