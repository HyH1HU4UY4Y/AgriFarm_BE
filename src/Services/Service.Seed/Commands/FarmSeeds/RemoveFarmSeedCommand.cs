using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Seed.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.FarmSeeds
{
    public class RemoveFarmSeedCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveFarmSeedCommandHandler : IRequestHandler<RemoveFarmSeedCommand, Guid>
    {
        private ISQLRepository<FarmSeedContext, FarmSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveFarmSeedCommandHandler> _logger;
        private IBus _bus;

        public RemoveFarmSeedCommandHandler(ISQLRepository<FarmSeedContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<RemoveFarmSeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit,
            IBus bus)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<Guid> Handle(RemoveFarmSeedCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _seeds.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _seeds.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);
            await _bus.ReplicateSeed(item, EventState.SoftDelete);

            return item.Id;
        }
    }
}
