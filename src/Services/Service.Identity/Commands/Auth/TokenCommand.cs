using Infrastructure.Identity.Contexts;
using SharedApplication.Authorize.Contracts;
using SharedApplication.CQRS;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Auth
{
    public record AuthorizeResponse(string Token, UserInforResponse UserInfo, bool IsSuccess = true);
    public record UserInforResponse(string UserName, string SiteId, string SiteCode, List<string> Roles);

    public record TokenCommand(string UserName, string Password, string? SiteCode = null): ICommand<AuthorizeResponse>;


    public class TokenCommandHandler : ICommandHandler<TokenCommand, AuthorizeResponse>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        private readonly ISQLRepository<IdentityContext, Site> _sites;

        public TokenCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, ISQLRepository<IdentityContext, Site> sites)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
            _sites = sites;
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

            var site = await _sites.GetOne(e=>e.Id.ToString() == user.Info.SiteId.ToString());

            string token = _tokenGenerator.GenerateJwt(user!.Info, user.Roles, user.Claims);

            return new AuthorizeResponse
            (

                token,
                new(
                    user.Info.UserName,
                    user.Roles.Any(e=>e == Roles.SuperAdmin) ? "root": user.Info.SiteId.ToString()!,
                    user.Roles.Any(e=>e == Roles.SuperAdmin) ? "root": site.SiteCode,
                    user.Roles
                    
                )
            );
            //throw new NotImplementedException();
        }
    }
}
