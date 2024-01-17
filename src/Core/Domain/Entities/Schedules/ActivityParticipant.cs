using SharedDomain.Defaults;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.Schedules
{
    public class ActivityParticipant : BaseEntity
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
/*
        public Guid ParticipantId { get; set; }
        public Member Participant { get; set; }
*/
        [Column(nameof(ParticipantId))]
        public Guid ParticipantId { get; set; }
        public MinimalUserInfo Participant { get; set; }

        public string Role { get; set; } = ActivityRole.Assignee.ToString();



    }
}
