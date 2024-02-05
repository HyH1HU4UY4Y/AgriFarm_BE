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
    public class CreateHarvestProductCommand : IRequest<Guid>
    {
        public Guid? SiteId { get; set; }
        public HarvestProductRequest HarvestProduct { get; set; }
    }

    public class CreateHarvestProductCommandHandler : IRequestHandler<CreateHarvestProductCommand, Guid>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<CreateHarvestProductCommandHandler> _logger;

        public CreateHarvestProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<CreateHarvestProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<Guid> Handle(CreateHarvestProductCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<HarvestProduct>(request.HarvestProduct);

            /*
            TODO:
                - Authorize and check for each site
            */
            if (request.SiteId == null)
            {
                //temp
                item.SiteId = new Guid(TempData.FarmId);
            }
            

            await _products.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;

        }
    }
}
