using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Newtonsoft.Json;
using Service.Seed.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Seed.Commands.FarmSeeds
{
    public class AddFarmSeedCommand : IRequest<Guid>
    {
        public SeedRequest Seed { get; set; }
    }

    public class AddFarmSeedCommandHandler : IRequestHandler<AddFarmSeedCommand, Guid>
    {
        private ISQLRepository<SeedlingContext, FarmSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddFarmSeedCommandHandler> _logger;

        public AddFarmSeedCommandHandler(ISQLRepository<SeedlingContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<AddFarmSeedCommandHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddFarmSeedCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with ref seed info
            */

            var item = _mapper.Map<FarmSeed>(request.Seed);

            await _seeds.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
