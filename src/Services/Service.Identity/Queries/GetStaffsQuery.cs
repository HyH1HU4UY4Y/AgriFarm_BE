using AutoMapper;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;

namespace Service.Identity.Queries
{
    public class GetStaffsQuery: IRequest<List<StaffResponse>>
    {
        public Guid SiteId { get; set; }
    }

    public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery, List<StaffResponse>>
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

        public async Task<List<StaffResponse>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _identity.FindAccounts(e => e.SiteId == request.SiteId);

            return _mapper.Map<List<StaffResponse>>(staffs);

        }
    }
}
