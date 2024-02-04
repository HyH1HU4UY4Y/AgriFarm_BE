using SharedApplication.Authorize.Contracts;
using Microsoft.IdentityModel.Tokens;
using SharedDomain.Defaults;
using SharedDomain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedApplication.Authorize.Services
{
    public class TokenGenerator : ITokenGenerator
    {

        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly long _expiredIn;

        public TokenGenerator(string key, string issueer, string audience, long expiredIn)
        {
            _key = key;
            _issuer = issueer;
            _audience = audience;
            _expiredIn = expiredIn;
        }



        public string GenerateJwt(Member user) =>
            GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user));

        private IEnumerable<Claim> GetClaims(Member user) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
            new("fullName", $"{user.FullName}"),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
        };

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(AppToken.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(AppToken.ExpiredIn),
               issuer: AppToken.Issuer,
               audience: AppToken.Audience,
               signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
