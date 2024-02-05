using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.FarmComponents.Common
{
    public class ReferencedFertilize : BaseEntity
    {
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
        public string? Manufactory { get; set; }
        public DateTime? ManufactureDate { get; set; }
        private string? _property;
        [MaxLength(8000)]
        public string? Property { get; set; }
        [NotMapped]
        public List<PropertyValue> Properties
        {
            get => JsonConvert.DeserializeObject<List<PropertyValue>>(_property!) ?? new();
            set => _property = JsonConvert.SerializeObject(value);
        }
        [MaxLength(8000)]
        public string? Notes { get; set; }
        [MaxLength(8000)]
        public string? Resources { get; set; }
        public ICollection<FarmFertilize> InUse { get; set; }

    }
}
