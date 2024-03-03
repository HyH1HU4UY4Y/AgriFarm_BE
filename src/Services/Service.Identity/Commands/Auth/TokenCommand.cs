using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedApplication.CQRS;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Auth
{
    

    public record TokenCommand(string UserName, string Password, string? SiteCode = null): IRequest<AuthorizeResponse>;


    public class TokenCommandHandler : IRequestHandler<TokenCommand, AuthorizeResponse>
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

            if (result.IsLockedOut)
            {
                throw new BadRequestException("This accout was locked.");
            }

            if (!result.Succeeded)
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
                new() {
                    Id = user.Info.Id,
                    UserName = user.Info.UserName,
                    Email = user.Info.Email,
                    FullName = $"{user.Info.FirstName} {user.Info.LastName}",
                    SiteId =  user.Roles.Any(e=>e == Roles.SuperAdmin) ? "root": user.Info.SiteId.ToString()!,
                    SiteCode = user.Roles.Any(e=>e == Roles.SuperAdmin) ? "root": site.SiteCode,
                    SiteName = user.Roles.Any(e => e == Roles.SuperAdmin) ? "root" : site.Name,
                    Role = user.Roles.Any(r => r == Roles.SuperAdmin)?Roles.SuperAdmin: user.Roles.First()
                    
                }
            );
            //throw new NotImplementedException();
        }
    }
}
