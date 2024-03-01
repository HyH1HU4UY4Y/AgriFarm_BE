using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Queries.FarmSeeds
{
    public class GetFarmSeedByIdQuery : IRequest<SeedResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetFarmSeedByIdQueryHandler : IRequestHandler<GetFarmSeedByIdQuery, SeedResponse>
    {

        private ISQLRepository<FarmSeedContext, FarmSeed> _seeds;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmSeedByIdQueryHandler> _logger;

        public GetFarmSeedByIdQueryHandler(ISQLRepository<FarmSeedContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<GetFarmSeedByIdQueryHandler> logger,
            IUnitOfWork<FarmSeedContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<SeedResponse> Handle(GetFarmSeedByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id, ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
