using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Seed.Contexts;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using Service.Seed.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Seed.Commands.FarmSeeds
{
    public class AddFarmSeedCommand : IRequest<SeedResponse>
    {
        public SeedCreateRequest Seed { get; set; }
        public Guid SiteId { get; set; }
    }

    public class AddFarmSeedCommandHandler : IRequestHandler<AddFarmSeedCommand, SeedResponse>
    {
        private ISQLRepository<FarmSeedContext, FarmSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<AddFarmSeedCommandHandler> _logger;
        private IBus _bus;

        public AddFarmSeedCommandHandler(ISQLRepository<FarmSeedContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<AddFarmSeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit,
            IBus bus)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<SeedResponse> Handle(AddFarmSeedCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                //- check for super admin
                - check integrated with ref seed info
            */

            var item = _mapper.Map<FarmSeed>(request.Seed);
            item.SiteId = request.SiteId;

            await _seeds.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateSeed(item, EventState.Add);

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
