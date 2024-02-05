using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Commands.Users
{
    public class CreateMemberCommand : IRequest<UserResponse>
    {
        public Guid SiteId { get; set; }
        public AddStaffRequest Staff { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, UserResponse>
    {
        private IIdentityService _identity;
        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<CreateMemberCommandHandler> _logger;

        public CreateMemberCommandHandler(IIdentityService identity,
            IMapper mapper, ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork, ILogger<CreateMemberCommandHandler> logger)
        {
            _identity = identity;
            _mapper = mapper;
            _sites = sites;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UserResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            if (_sites.GetOne(e => e.Id == request.SiteId).Result == null)
                throw new BadRequestException("Invalid Site Id");

            var account = _mapper.Map<Member>(request.Staff);
            account.Email = request.Staff.UserName;
            account.SiteId = request.SiteId;

            var user = await _identity.CreateUserInType(account, request.Staff.Password , request.AccountType);

            if (user == null)
            {
                throw new Exception("Fail to add user!");
            }

            var rs = _mapper.Map<UserResponse>(user);
            rs.Role = request.AccountType.ToString();

            return rs;
        }
    }
}
