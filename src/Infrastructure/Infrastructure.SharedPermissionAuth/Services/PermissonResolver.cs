using Infrastructure.SharedPermissionAuth.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SharedPermissionAuth.Services
{
    internal class PermissonResolver<Tdb> : IPermissonResolver where Tdb : IdentityDbContext<Member, IdentityRole<Guid>, Guid>
    {
        /*private readonly Tdb _db;

        public async Task<bool> HasPermissionAsync(string userId, string permission)
        {
            var user = await _db.Users.FirstOrDefaultAsync(e=>e.Id.ToString() == userId);

            //_ = user ?? throw new UnauthorizedException("Authentication Failed.");

            if (user == null) return false;

            var dbur = await _db.UserRoles.AsNoTracking().ToListAsync();
            var userRoles = await _db.Roles
                .Where(e => dbur.Any(ur => ur.UserId == user.Id && ur.RoleId == e.Id))
                .ToListAsync();
                                ;
            var permissions = new List<string>();
            foreach (var role in _db.Roles
                .Where(r => userRoles.Contains(r.Name!))
                .ToListAsync()))
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                permissions.AddRange(claims
                    .Where(rc => rc.Type.ToLower() == "permission")
                    .Select(rc => rc.Value!)
                    .ToList());
            }

            throw new NotImplementedException();
        }*/
    }
}
