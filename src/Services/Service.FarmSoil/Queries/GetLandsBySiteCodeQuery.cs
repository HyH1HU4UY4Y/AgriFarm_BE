using AutoMapper;
using Infrastructure.Soil.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Soil.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Queries
{
    public class GetLandsBySiteCodeQuery: IRequest<PagedList<LandResponse>>
    {
        public PaginationRequest? Pagination { get; set; } = new();
        public Guid SiteId { get; set; }
    }

    public class GetLandsBySiteCodeQueryHandler : IRequestHandler<GetLandsBySiteCodeQuery, PagedList<LandResponse>>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<GetLandsBySiteCodeQueryHandler> _logger;
        private IMapper _mapper;

        public GetLandsBySiteCodeQueryHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<GetLandsBySiteCodeQueryHandler> logger,
            IMapper mapper)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PagedList<LandResponse>> Handle(GetLandsBySiteCodeQuery request, CancellationToken cancellationToken)
        {
            var items = await _lands.GetMany(
                e => e.SiteId == request.SiteId
                , ls => ls.Include(x => x.Site)
                );

            var rs = PagedList<LandResponse>.ToPagedList(
                    _mapper.Map<List<LandResponse>>(items),
                    request.Pagination!.PageNumber,
                    request.Pagination!.PageSize
                );
            

            return rs;
        }
    }
}
