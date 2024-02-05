using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.Commands;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Queries
{
    public class GetMemberByIdQuery: IRequest<UserDetailResponse>
    {
        public Guid? SiteId { get; set; }
        public Guid UserId { get; set; } 
    }

    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, UserDetailResponse>
    {
        private IIdentityService _identity;

        

        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<GetMemberByIdQueryHandler> _logger;

        public GetMemberByIdQueryHandler(IIdentityService identity,
            ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork,
            IMapper mapper,
            ILogger<GetMemberByIdQueryHandler> logger)
        {
            _identity = identity;
            _sites = sites;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDetailResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _identity.GetFullMember(e => e.Id == request.UserId
                        && e.SiteId == request.SiteId);

            if(user == null)
            {
                throw new NotFoundException("Not Found!");
            }

            var rs = _mapper.Map<UserDetailResponse>(user.Info);
            rs.Role = user.Roles.Count < 4 ? user.Roles.First() : Roles.SuperAdmin;

            return rs;

        }
    }
}
