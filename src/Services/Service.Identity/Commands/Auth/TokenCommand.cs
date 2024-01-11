using SharedApplication.Authorize.Contracts;
using SharedApplication.CQRS;
using SharedDomain.Exceptions;

namespace Service.Identity.Commands.Auth
{
    public record AuthorizeResponse(string Token, UserInforResponse UserInfo);
    public record UserInforResponse(string Name, List<string> Roles);

    public record TokenCommand(string UserName, string Password): ICommand<AuthorizeResponse>;


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

            var (user, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJwt(user);

            return new AuthorizeResponse
            (
                
                token,
                new (
                    user.UserName,
                    roles.ToList()
                )
            );
        }
    }
}
