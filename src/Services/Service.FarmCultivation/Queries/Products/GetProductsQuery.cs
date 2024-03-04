using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmCultivation.DTOs.Products;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Products
{
    public class GetProductsQuery : IRequest<PagedList<ProductResponse>>
    {
        public Guid SeasonId { get; set; }
        public Guid SiteId { get; set; }
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedList<ProductResponse>>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<GetProductsQueryHandler> _logger;

        public GetProductsQueryHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<GetProductsQueryHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<PagedList<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            var season = await _seasons.GetOne(e => e.Id == request.SeasonId && e.SiteId == request.SiteId);
            if(season == null)
            {
                throw new NotFoundException();
            }

            var items = await _products.GetMany(e => e.SeasonId == request.SeasonId,
                                                ls => ls.Include(x => x.Season)
                                                    .Include(x => x.Land)
                                                    .Include(x=>x.Seed));


            return PagedList<ProductResponse>.ToPagedList(
                    _mapper.Map<List<ProductResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }



}
