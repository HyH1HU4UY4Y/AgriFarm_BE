using SharedApplication.Authorize.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using ValidationException = SharedDomain.Exceptions.ValidationException;
using SharedDomain.Defaults;
using System.Security.Claims;
using System.Linq.Expressions;

namespace SharedApplication.Authorize.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public IdentityService(RoleManager<IdentityRole<Guid>> roleManager, SignInManager<Member> signInManager, UserManager<Member> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }



        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;


        }

        public Member? FindAccount(Func<Member, bool> filter)
        {
            return _userManager.Users.FirstOrDefault(filter);

        }

        public async Task<List<Member>> FindAccounts(Func<Member, bool> filter = null)
        {
            var users = await _userManager.Users.ToListAsync();
            if(filter != null)
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
            var account = _userManager.Users.FirstOrDefault(filter);
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

            return new() { 
                Info = account,
                Roles = urs,
                Claims = claims
            };

        }

        public async Task<Member?> CreateUserInTypes(Member member, AccountType type = AccountType.Member)
        {

            var rs = await _userManager.CreateAsync(member);
            if (!rs.Succeeded) return null;
            var roles = await _roleManager.Roles.ToListAsync();

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
                    await _userManager.AddToRoleAsync(member, Roles.SuperAdmin);
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

        public async Task<bool> UpdateUserProfile(Member member, IList<string> roles)
        {
            var user = await _userManager.FindByIdAsync(member.Id.ToString());

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

    }
}
