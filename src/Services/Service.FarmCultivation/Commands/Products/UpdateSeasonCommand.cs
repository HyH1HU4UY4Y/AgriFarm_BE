using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Commands.Products
{
    public class UpdateHarvestProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public HarvestProductRequest HarvestProduct { get; set; }
    }

    public class UpdateHarvestProductCommandHandler : IRequestHandler<UpdateHarvestProductCommand, Guid>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<UpdateHarvestProductCommandHandler> _logger;

        public UpdateHarvestProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<UpdateHarvestProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<Guid> Handle(UpdateHarvestProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.HarvestProduct, item);

            await _products.UpdateAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;
        }
    }
}
