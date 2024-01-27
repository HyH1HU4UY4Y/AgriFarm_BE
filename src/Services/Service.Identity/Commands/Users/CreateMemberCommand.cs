using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Commands.Users
{
    public class CreateMemberCommand : IRequest<Guid>
    {
        public Guid SiteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
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

        public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            if (_sites.GetOne(e => e.Id == request.SiteId).Result == null)
                throw new BadRequestException("Invalid Site Id");

            var account = _mapper.Map<Member>(request);
            account.Email = request.UserName;

            var rs = await _identity.CreateUserInType(account, request.Password, request.AccountType);

            if (rs == null)
            {
                throw new Exception("Fail to add user!");
            }

            return rs.Id;
        }
    }
}
