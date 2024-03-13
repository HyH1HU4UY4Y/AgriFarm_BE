using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using SharedDomain.Exceptions;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using SharedDomain.Defaults;

namespace Service.FarmRegistry.Commands
{
    public record RegistFarmCommand
        (string? FirstName
        , string? LastName, string Phone
        , string Email, string Address, string SiteCode
        , string SiteName, Guid SolutionId
        , string PaymentDetail) :IRequest<RegisterFormResponse>;
        
    public class RegistFarmCommandhandler: IRequestHandler<RegistFarmCommand, RegisterFormResponse>
    {
        private readonly ISQLRepository<RegistrationContext, FarmRegistration> _forms;
        private readonly ISQLRepository<RegistrationContext, PackageSolution> _packages;
        private readonly ISQLRepository<RegistrationContext, Site> _sites;
        private readonly ISQLRepository<RegistrationContext, MinimalUserInfo> _users;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;

        public RegistFarmCommandhandler(IMapper mapper,
            IUnitOfWork<RegistrationContext> unitOfWork,
            ISQLRepository<RegistrationContext, FarmRegistration> forms
,
            ISQLRepository<RegistrationContext, PackageSolution> packages,
            ISQLRepository<RegistrationContext, Site> sites,
            ISQLRepository<RegistrationContext, MinimalUserInfo> users)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _forms = forms;
            _packages = packages;
            _sites = sites;
            _users = users;
        }

        public async Task<RegisterFormResponse> Handle(RegistFarmCommand request, CancellationToken cancellationToken)
        {
            string msg = string.Empty;

            var solution = _packages.GetOne(e => e.Id == request.SolutionId).Result;
            if (solution == null) throw new BadRequestException("Solution not exist.");

            var site = _sites.GetOne(e => e.Name == request.SiteName).Result;
            var user = _users.GetOne(e => e.UserName == request.Email).Result;
            var forms = _forms.GetMany(e=>e.SiteName == request.SiteName
                                        || e.SiteCode == request.SiteCode
                                        || e.Email == request.Email
                                        ).Result??new();
            if (site?.Name == request.SiteName
                || forms.Any(e => e.SiteName == request.SiteName
                            && e.IsApprove != DecisonOption.No)
                )
            {
                msg += "Farm Name not valid.";
            }

             if (site?.SiteCode == request.SiteCode
                 || forms.Any(e => e.SiteCode == request.SiteCode
                             && e.IsApprove != DecisonOption.No)
                 )
            {
                msg += " Farm Code not valid.";
            }

             if (user?.UserName == request.Email
                || forms.Any(e => e.Email.Equals(request.Email)
                            && e.IsApprove != DecisonOption.No)
                )
            {
                msg += " Email not valid.";
            }
           

            if (!string.IsNullOrEmpty(msg.Trim()))
            {
                throw new BadRequestException(msg.Trim());
            }
          


            var entity = _mapper.Map<FarmRegistration>(request);
            entity.Cost = solution.Price;
            await _forms.AddAsync(entity);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return  _mapper.Map<RegisterFormResponse>(entity);
        }
    }
}
