using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Queries.RefSeeds
{
    public class GetRefSeedByIdQuery : IRequest<RefSeedResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetRefSeedByIdQueryHandler : IRequestHandler<GetRefSeedByIdQuery, RefSeedResponse>
    {
        private ISQLRepository<SeedlingContext, ReferencedSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefSeedByIdQueryHandler> _logger;

        public GetRefSeedByIdQueryHandler(ISQLRepository<SeedlingContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<GetRefSeedByIdQueryHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }


        public async Task<RefSeedResponse> Handle(GetRefSeedByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e=>e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist!");
            }


            return _mapper.Map<RefSeedResponse>(item);
        }
    }
}
