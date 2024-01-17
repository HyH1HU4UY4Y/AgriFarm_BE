using SharedDomain.Entities.Users;

namespace SharedApplication.Authorize.Contracts
{
    public interface ITokenGenerator
    {
        string GenerateJwt(Member user, Dictionary<string, string> scopes = null);
    }
}