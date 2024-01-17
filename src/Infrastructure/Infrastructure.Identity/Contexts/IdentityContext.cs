using Infrastructure.Identity.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using System.Collections.Immutable;

namespace Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<Member, IdentityRole<Guid>, Guid>
    {
        

        public IdentityContext(DbContextOptions options) : base(options)
        {
        }

        protected IdentityContext()
        {
        }

        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<Site> SiteDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(SiteDetailConfig).Assembly);
            //builder.ApplyConfigurationsFromAssembly(typeof(RoleConfig).Assembly);
            SeedInitData(builder);
        }

        private void SeedInitData(ModelBuilder builder)
        {
            var users = new List<Member>{
                new()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Super Admin 01",
                    Email = "admin01@system",
                    NormalizedEmail= "admin01@system".ToUpper(),
                    UserName = "admin01@system",
                    NormalizedUserName = "admin01@system".ToUpper(),
                    PhoneNumber = "0122222222",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SiteId = null,
                    SecurityStamp = Guid.NewGuid().ToString(),
                }
            };

            var hasher = new PasswordHasher<Member>();
            users[0].PasswordHash = hasher.HashPassword(users[0], "@123456");

            

            var roles = Enum.GetNames<AccountType>().ToList()
                .Select(e => new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = e,
                    NormalizedName = e.ToUpper(),
                })
                .ToImmutableList();

            List<IdentityUserRole<Guid>> ur = new();
            roles.ForEach(e =>
                ur.Add(new()
                {

                    RoleId = e.Id,
                    UserId = users[0].Id
                })
            );

            builder.Entity<Member>().HasData(users);
            builder.Entity<IdentityRole<Guid>>().HasData(roles);
            builder.Entity<IdentityUserRole<Guid>>().HasData(ur);
        }

    }
}
