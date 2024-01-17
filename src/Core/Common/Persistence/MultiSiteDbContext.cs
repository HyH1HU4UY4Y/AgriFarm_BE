using Application.CommonExtensions;
using Microsoft.EntityFrameworkCore;
using SharedApplication.MultiTenant.Implement;
using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence
{
    public abstract class MultiSiteDbContext: BaseDbContext
    {
        protected readonly string _siteId;


        protected MultiSiteDbContext(DbContextOptions options) : base(options)
        {
            
            _siteId = "none";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.SetQueryFilterOnAllEntities<IMultiSite>(e => e.SiteId.ToString() == _siteId);
        }
    }
}
