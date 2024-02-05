using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Users
{
    public class UpdateMemberCommand : IRequest<UserDetailResponse>
    {
        public Guid Id { get; set; }
        public SaveMemberDetailRequest User { get; set; }
    }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, UserDetailResponse>
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

        public async Task<UserDetailResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var user = _identity.FindAccount(e => e.Id == request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not valid!");

            }

            _mapper.Map(request.User, user);

            await _identity.UpdateUserProfile(user);

            return _mapper.Map<UserDetailResponse>(user);
        }
    }

}
