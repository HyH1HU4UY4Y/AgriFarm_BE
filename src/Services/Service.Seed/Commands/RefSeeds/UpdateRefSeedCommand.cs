using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Service.Seed.Commands.RefSeeds;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.RefSeeds
{
    public class UpdateRefSeedCommand : IRequest<RefSeedResponse>
    {
        public Guid Id { get; set; }
        public RefSeedRequest Seed { get; set; }
    }

    public class UpdateRefSeedCommandHandler : IRequestHandler<UpdateRefSeedCommand, RefSeedResponse>
    {
        private ISQLRepository<SeedlingContext, ReferencedSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateRefSeedCommandHandler> _logger;

        public UpdateRefSeedCommandHandler(ISQLRepository<SeedlingContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<UpdateRefSeedCommandHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<RefSeedResponse> Handle(UpdateRefSeedCommand request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Seed, item);

            await _seeds.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RefSeedResponse>(item);
        }
    }
}
