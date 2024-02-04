using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Users
{
    public class UpdateMemberCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? IdentificationCard { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Education { get; set; }
        public DateTime? DOB { get; set; }
    }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Guid>
    {
        private IIdentityService _identity;
        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<UpdateMemberCommandHandler> _logger;

        public UpdateMemberCommandHandler(IIdentityService identity,
            IMapper mapper, ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork, ILogger<UpdateMemberCommandHandler> logger)
        {
            _identity = identity;
            _mapper = mapper;
            _sites = sites;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var user = _identity.FindAccount(e => e.Id == request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not valid!");

            }

            _mapper.Map(request, user);

            await _identity.UpdateUserProfile(user);

            return user.Id;
        }
    }

}
