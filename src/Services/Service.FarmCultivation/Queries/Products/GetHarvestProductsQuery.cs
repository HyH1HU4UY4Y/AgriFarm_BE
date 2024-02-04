using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmCultivation.DTOs.Products;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Products
{
    public class GetHarvestProductsQuery : IRequest<PagedList<HarvestProductResponse>>
    {
        public Guid SeasonId { get; set; }
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetHarvestProductsQueryHandler : IRequestHandler<GetHarvestProductsQuery, PagedList<HarvestProductResponse>>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<GetHarvestProductsQueryHandler> _logger;

        public GetHarvestProductsQueryHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<GetHarvestProductsQueryHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<PagedList<HarvestProductResponse>> Handle(GetHarvestProductsQuery request, CancellationToken cancellationToken)
        {


            var items = await _products.GetMany(e => e.SeasonId == request.SeasonId,
                                                //&& !e.Season.IsDeleted,
                                                ls => ls.Include(x => x.Season)
                                                    .Include(x => x.Land));


            return PagedList<HarvestProductResponse>.ToPagedList(
                    _mapper.Map<List<HarvestProductResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }



}
