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
    public class GetLandBySiteCodeQuery: IRequest<PagedList<LandResponse>>
    {
        public PaginationRequest? Pagination { get; set; } = new();
        public string SiteCode { get; set; }
    }

    public class GetLandBySiteCodeQueryHandler : IRequestHandler<GetLandBySiteCodeQuery, PagedList<LandResponse>>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<GetLandBySiteCodeQueryHandler> _logger;
        private IMapper _mapper;

        public GetLandBySiteCodeQueryHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<GetLandBySiteCodeQueryHandler> logger,
            IMapper mapper)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PagedList<LandResponse>> Handle(GetLandBySiteCodeQuery request, CancellationToken cancellationToken)
        {
            var items = await _lands.GetMany(
                e => e.Site.SiteCode == request.SiteCode, ls => ls.Include(x => x.Site)
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
