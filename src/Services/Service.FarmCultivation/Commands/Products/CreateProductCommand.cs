using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Defaults;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Commands.Products
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public Guid SiteId { get; set; }
        public Guid SeasonId { get; set; }
        public ProductRequest Product { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<CreateProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - check valid time
            */

            var item = _mapper.Map<HarvestProduct>(request.Product);
            item.SiteId = request.SiteId;
            item.SeasonId = request.SeasonId;
            item.Name = $"{item.Seed.Name} ({item.Land.Name})";
            item.SeedId = item.Seed.Id;
            item.LandId = item.Land.Id;
            item.Seed = null;
            item.Land = null;

            await _products.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductResponse>(item);

        }
    }
}
