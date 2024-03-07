using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<GetProductByIdQueryHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id,
                                        ls => ls.Include(x => x.Season)
                                              .Include(x => x.Land)
                                              .Include(x=>x.Seed));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<ProductResponse>(item);

        }
    }
}
