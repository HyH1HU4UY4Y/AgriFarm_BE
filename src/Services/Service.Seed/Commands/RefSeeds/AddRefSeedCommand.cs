using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Service.Seed.Commands.RefSeeds;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.RefSeeds
{
    public class AddRefSeedCommand : IRequest<Guid>
    {
        public RefSeedRequest Seed { get; set; }
    }

    public class AddRefSeedCommandHandler : IRequestHandler<AddRefSeedCommand, Guid>
    {
        private ISQLRepository<SeedlingContext, ReferencedSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddRefSeedCommandHandler> _logger;

        public AddRefSeedCommandHandler(ISQLRepository<SeedlingContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<AddRefSeedCommandHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddRefSeedCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for each bussiness role
                - check integrated with ref seed info
            */

            var item = _mapper.Map<ReferencedSeed>(request.Seed);

            await _seeds.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
