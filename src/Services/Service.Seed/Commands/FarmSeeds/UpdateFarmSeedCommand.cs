using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Seed.Contexts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.FarmSeeds
{
    public class UpdateFarmSeedCommand : IRequest<SeedResponse>
    {
        public Guid Id { get; set; }
        public SeedInfoRequest Seed { get; set; }
    }

    public class UpdateFarmSeedCommandHandler : IRequestHandler<UpdateFarmSeedCommand, SeedResponse>
    {
        private ISQLRepository<FarmSeedContext, FarmSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmSeedCommandHandler> _logger;
        private IBus _bus;

        public UpdateFarmSeedCommandHandler(ISQLRepository<FarmSeedContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<UpdateFarmSeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit,
            IBus bus)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<SeedResponse> Handle(UpdateFarmSeedCommand request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id,
                                           ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Seed, item);

            await _seeds.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateSeed(item, EventState.Modify);

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
