using Microsoft.EntityFrameworkCore;
using SharedApplication.MultiTenant.Implement;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FarmCultivation.Contexts
{
    public class CultivationContext : MultiSiteDbContext
    {
        public CultivationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CultivationSeason> Seasons { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<BaseComponent> Components { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }
        public DbSet<Member> Participants { get; set; }
        //public DbSet<>

    }
}
