using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Commands.Products
{
    public class DeleteHarvestProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHarvestProductCommandHandler : IRequestHandler<DeleteHarvestProductCommand, Guid>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<DeleteHarvestProductCommandHandler> _logger;

        public DeleteHarvestProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<DeleteHarvestProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<Guid> Handle(DeleteHarvestProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _products.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;

        }
    }
}
