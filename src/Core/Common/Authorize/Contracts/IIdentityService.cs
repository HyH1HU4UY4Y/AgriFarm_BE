using Microsoft.AspNetCore.Identity;
using SharedApplication.Authorize.Services;
using SharedDomain.Defaults;
using SharedDomain.Entities.Users;

namespace SharedApplication.Authorize.Contracts
{
    public interface IIdentityService
    {
        Task AddScopes(Member member, string[] scope);
        Task<Member?> CreateUserInType(Member member, string password, AccountType type = AccountType.Member);
        Member? FindAccount(Func<Member, bool> filter);
        Task<List<Member>> FindAccounts(Func<Member, bool> filter = null);
        Task<FullUserResult?> GetFullMember(Func<Member, bool> filter);
        IEnumerable<(Member member, List<string> roles)> GetMembersWithRoles(Func<Member, bool> filter = null);
        Task<bool> IsInRoleAsync(Func<Member, bool> filter, string role);
        Task<bool> IsUniqueUserName(string userName);
        Task<IdentityResult> RawDeleteAsync(Member user);
        Task<IdentityResult> SetLock(Guid id, bool isLock);
        Task<SignInResult> SigninUserAsync(string userName, string password);
        Task<IdentityResult> UpdateUserProfile(Member member, IList<string> roles = null);
    }
}