using AutoMapper;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedApplication.Pagination;
using SharedDomain.Defaults;
using SharedDomain.Entities.Users;

namespace Service.Identity.Queries
{
    public class GetStaffsQuery: IRequest<PagedList<UserResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId { get; set; }
    }

    public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery, PagedList<UserResponse>>
    {
        private readonly IIdentityService _identity;
        private IMapper _mapper;
        private ILogger<GetStaffsQueryHandler> _logger;

        public GetStaffsQueryHandler(IIdentityService identity, IMapper mapper, ILogger<GetStaffsQueryHandler> logger)
        {
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<UserResponse>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
        {
            var staffs = _identity.GetMembersWithRoles(e => e.SiteId == request.SiteId);

            var rs = new List<UserResponse>();

            foreach (var staff in staffs)
            {
                var u  = _mapper.Map<UserResponse>(staff.member);
                u.Role = staff.roles.Any(r => r == Roles.SuperAdmin) ? Roles.SuperAdmin : staff.roles.First();
                rs.Add(u);
            };

            return PagedList<UserResponse>
                .ToPagedList(rs, 
                request.Pagination!.PageNumber, 
                request.Pagination.PageSize);

        }
    }
}
