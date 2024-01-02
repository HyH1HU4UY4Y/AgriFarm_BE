using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Base;

namespace SharedDomain.Subscribe
{
    public class PackageSolution : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long Duration { get; set; }

        public ICollection<Subscripton> Subscripts { get; set; }

    }
}
