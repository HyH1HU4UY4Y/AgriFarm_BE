using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Base
{
    public abstract class BaseEntity : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
    }
}
