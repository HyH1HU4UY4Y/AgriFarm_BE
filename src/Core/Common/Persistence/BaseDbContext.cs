using Application.CommonExtensions;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext()
        {
        }

        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }


        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries<ITraceableItem>())
            {
                switch(history.State){
                    case EntityState.Deleted:
                        history.Entity.IsDeleted = true;
                        history.Entity.DeletedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        history.Entity.LastModify = DateTime.Now;
                        break;
                }
                
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var history in this.ChangeTracker.Entries<ITraceableItem>())
            {
                switch (history.State)
                {
                    case EntityState.Deleted:
                        history.Entity.IsDeleted = true;
                        history.Entity.DeletedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        history.Entity.LastModify = DateTime.Now;
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //common string field lenght
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(150)");
            }

            //set delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {

                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }


            //modelBuilder.SetQueryFilterOnAllEntities<ITraceableItem>(e=>!e.IsDeleted);
        }
    }
}
