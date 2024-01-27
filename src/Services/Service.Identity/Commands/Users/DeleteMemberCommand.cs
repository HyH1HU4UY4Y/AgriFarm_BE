using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Users
{
    public class DeleteMemberCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Guid>
    {
        private IIdentityService _identity;
        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<DeleteMemberCommandHandler> _logger;

        public DeleteMemberCommandHandler(IIdentityService identity,
            IMapper mapper, ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork, ILogger<DeleteMemberCommandHandler> logger)
        {
            _identity = identity;
            _mapper = mapper;
            _sites = sites;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var user = _identity.FindAccount(e => e.Id == request.Id);
            if (user == null)
            {
                throw new BadHttpRequestException("User not valid!");

            }

            await _identity.RawDeleteAsync(user);

            return user.Id;
        }
    }
}
