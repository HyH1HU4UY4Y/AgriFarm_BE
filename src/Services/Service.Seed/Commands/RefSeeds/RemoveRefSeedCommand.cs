using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Service.Seed.Commands.RefSeeds;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.RefSeeds
{
    public class RemoveRefSeedCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveRefSeedCommandHandler : IRequestHandler<RemoveRefSeedCommand, Guid>
    {
        private ISQLRepository<FarmSeedContext, ReferencedSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveRefSeedCommandHandler> _logger;

        public RemoveRefSeedCommandHandler(ISQLRepository<FarmSeedContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<RemoveRefSeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveRefSeedCommand request, CancellationToken cancellationToken)
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

            return item.Id;
        }
    }
}
