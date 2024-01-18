using SharedDomain.Entities.Users;

namespace SharedApplication.Authorize.Contracts
{
    public interface ITokenGenerator
    {
        string GenerateJwt(Member user, List<string> roles = null, Dictionary<string, string> scopes = null);
    }
}