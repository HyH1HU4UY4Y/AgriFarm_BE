﻿using Infrastructure.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Authorize;
using SharedDomain.Entities.Users;

namespace Infrastructure.Identity
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddInfras(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<IdentityContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            services.AddIdentity<Member, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });


            services.AddAuthModule(configuration);

            return services;
        }
    }
}
