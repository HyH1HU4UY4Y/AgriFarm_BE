using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules.Training
{
    public class TrainingContent : BaseEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(8000)]
        public string Content { get; set; }
        [MaxLength(int.MaxValue)]
        public string Resource { get; set; }
    }
}
