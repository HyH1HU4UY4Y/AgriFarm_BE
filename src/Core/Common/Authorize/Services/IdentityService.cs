using SharedApplication.Authorize.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using ValidationException = SharedDomain.Exceptions.ValidationException;
using SharedDomain.Defaults;
using System.Security.Claims;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace SharedApplication.Authorize.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public ILogger<IdentityService> _logger;

        public IdentityService(RoleManager<IdentityRole<Guid>> roleManager,
            SignInManager<Member> signInManager,
            UserManager<Member> userManager,
            ILogger<IdentityService> logger)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }



        public async Task<SignInResult> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);

            if (result.IsLockedOut)
            {
                _logger.LogInformation($"User account {userName} is in lock. Cannot login!");
            }

            return result;


        }

        public Member? FindAccount(Func<Member, bool> filter)
        {
            return _userManager.Users.Where(e => !e.IsDeleted).FirstOrDefault(filter);

        }

        public async Task<List<Member>> FindAccounts(Func<Member, bool> filter = null)
        {
            var users = await _userManager.Users
                .Where(e => !e.IsDeleted)
                .ToListAsync();
            if (filter != null)
            {
                users = users.Where(filter).ToList();
            }

            return users;
        }

        public IEnumerable<(Member member, List<string> roles)> GetMembersWithRoles(Func<Member, bool> filter = null)
        {
            var accounts = _userManager.Users.ToList();
            if (filter != null)
            {
                accounts = accounts.Where(filter).ToList();
            }
            foreach (var account in accounts)
            {
                List<string> urs = (List<string>)_userManager.GetRolesAsync(account).Result;
                yield return (account, urs);
            }
        }

        public async Task<FullUserResult?> GetFullMember(Func<Member, bool> filter)
        {
            var account = _userManager.Users
                .Include(e => e.Certificates)
                .FirstOrDefault(filter);
            if (account == null) return null;
            var roles = _roleManager.Roles.ToList();

            Dictionary<string, string> claims = new Dictionary<string, string>();

            _userManager.GetClaimsAsync(account).Result.ToList().ForEach(c =>
            {
                claims.Add(c.Type, c.Value);
            });

            List<string> urs = (List<string>)_userManager.GetRolesAsync(account).Result;
            urs.ForEach(ur =>
            {
                _roleManager
                .GetClaimsAsync(roles.FirstOrDefault(r => r.Name == ur)!).Result
                .ToList()
                .ForEach(e => claims.Add(e.Type, e.Value));
            });

            return new()
            {
                Info = account,
                Roles = urs,
                Claims = claims
            };

        }

        public async Task<Member?> CreateUserInType(Member member, string password, AccountType type = AccountType.Member)
        {

            var rs = await _userManager.CreateAsync(member, password);
            if (!rs.Succeeded) return null;
            //var roles = await _roleManager.Roles.ToListAsync();

            switch (type)
            {
                case AccountType.Member:
                    await _userManager.AddToRoleAsync(member, Roles.Member);
                    break;
                case AccountType.Manager:
                    await _userManager.AddToRoleAsync(member, Roles.Manager);
                    break;
                case AccountType.Admin:
                    await _userManager.AddToRoleAsync(member, Roles.Admin);
                    break;
                case AccountType.SuperAdmin:
                    await _userManager.AddToRolesAsync(member, Enum.GetNames<AccountType>());
                    break;
            }

            return member;
        }

        public async Task AddScopes(Member member, string[] scope)
        {
            var user = await _userManager.FindByIdAsync(member.Id.ToString());
            if (user == null) return;
            var rs = await _userManager.AddClaimsAsync(user, scope.Select(s => new Claim(FarmClaimType.Scope, s)));
            if (!rs.Succeeded) return;

        }

        public async Task<bool> IsInRoleAsync(Func<Member, bool> filter, string role)
        {
            var user = _userManager.Users.FirstOrDefault(filter);

            if (user == null)
            {
                return false;
            }
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        public async Task<IdentityResult> UpdateUserProfile(Member member, IList<string> roles = null)
        {
            var user = await _userManager.FindByIdAsync(member.Id.ToString());

            user.LastModify = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return result;

            if(roles != null)
            {
                var uroles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, uroles);
                result = await _userManager.AddToRolesAsync(user, roles);
            }

            return result;
        }

        /*public async Task<IdentityResult> SoftDeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                throw new NotFoundException("User not exist!");
            }

            user.IsDeleted = true;
            user.DeletedDate = DateTime.Now;

            var result = await _userManager.UpdateAsync(user) ;

            return result;
        }*/


        public async Task<IdentityResult> SetLock(Guid id, bool isLock)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new NotFoundException("User not exist!");
            }

            var result = await _userManager.SetLockoutEnabledAsync(user, isLock);

            return result;
        }

        public async Task<IdentityResult> RawDeleteAsync(Member user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result;
        }
    }
}
