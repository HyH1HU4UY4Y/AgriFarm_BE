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
    public class AddRefSeedCommand : IRequest<RefSeedResponse>
    {
        public RefSeedRequest Seed { get; set; }
    }

    public class AddRefSeedCommandHandler : IRequestHandler<AddRefSeedCommand, RefSeedResponse>
    {
        private ISQLRepository<FarmSeedContext, ReferencedSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<AddRefSeedCommandHandler> _logger;

        public AddRefSeedCommandHandler(ISQLRepository<FarmSeedContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<AddRefSeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<RefSeedResponse> Handle(AddRefSeedCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for each bussiness role
                - check integrated with ref seed info
            */

            var item = _mapper.Map<ReferencedSeed>(request.Seed);

            await _seeds.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RefSeedResponse>(item);
        }
    }
}
