using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
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
        private ISQLRepository<SeedlingContext, FarmSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmSeedCommandHandler> _logger;

        public UpdateFarmSeedCommandHandler(ISQLRepository<SeedlingContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<UpdateFarmSeedCommandHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<SeedResponse> Handle(UpdateFarmSeedCommand request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Seed, item);

            await _seeds.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
