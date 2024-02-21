using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Common
{
    public class ReferencedPesticide : BaseEntity
    {
        public string Name { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        public string? Manufactory { get; set; }
        public DateTime? ManufactureDate { get; set; }
        private string? _property;
        public string? Property { get => _property; set => _property = value; }
        [NotMapped]
        public List<PropertyValue> Properties
        {
            get => !string.IsNullOrWhiteSpace(_property) ?
                JsonConvert.DeserializeObject<List<PropertyValue>>(_property)!
                : new();
            set => _property = JsonConvert.SerializeObject(value);
        }

        [MaxLength(8000)]
        public string? Notes { get; set; }
        [MaxLength(8000)]
        public string? Resources { get; set; }
        public ICollection<FarmPesticide> InUse { get; set; }

    }
}
