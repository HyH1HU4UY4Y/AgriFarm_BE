using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using Service.Water.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Water.Queries
{
    public class GetFarmWatersQuery : IRequest<PagedList<WaterResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId { get; set; }
    }

    public class GetFarmWatersQueryHandler : IRequestHandler<GetFarmWatersQuery, PagedList<WaterResponse>>
    {

        private ISQLRepository<FarmWaterContext, FarmWater> _waters;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmWatersQueryHandler> _logger;

        public GetFarmWatersQueryHandler(ISQLRepository<FarmWaterContext, FarmWater> waters,
            IMapper mapper,
            ILogger<GetFarmWatersQueryHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _waters = waters;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<WaterResponse>> Handle(GetFarmWatersQuery request, CancellationToken cancellationToken)
        {
            var items = await _waters.GetMany(e=>e.SiteId == request.SiteId);


            return PagedList<WaterResponse>.ToPagedList(
                    _mapper.Map<List<WaterResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
