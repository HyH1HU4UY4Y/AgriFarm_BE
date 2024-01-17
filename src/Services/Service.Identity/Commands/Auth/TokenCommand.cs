using SharedApplication.Authorize.Contracts;
using SharedApplication.CQRS;
using SharedDomain.Exceptions;

namespace Service.Identity.Commands.Auth
{
    public record AuthorizeResponse(string Token, UserInforResponse UserInfo, bool IsSuccess = true);
    public record UserInforResponse(string Name, List<string> Roles);

    public record TokenCommand(string UserName, string Password, string SiteKey = null): ICommand<AuthorizeResponse>;


    public class TokenCommandHandler : ICommandHandler<TokenCommand, AuthorizeResponse>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;

        public TokenCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthorizeResponse> Handle(TokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var user = _identityService.GetFullMember(e=>e.UserName == request.UserName).Result;
            if (user == null) {
                return new(null, null, false);
            }

            string token = _tokenGenerator.GenerateJwt(user!.Info, user.Claims);

            return new AuthorizeResponse
            (

                token,
                new(
                    user.Info.UserName,
                    user.Roles
                )
            );
            //throw new NotImplementedException();
        }
    }
}
