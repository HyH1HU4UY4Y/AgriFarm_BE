using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.Subscribe
{
    public class PackageSolution : BaseEntity
    {
        public string? Name { get; set; }
        [StringLength(5000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long? DurationInMonth { get; set; }

        public ICollection<Subscripton>? Subscripts { get; set; }

    }
}
