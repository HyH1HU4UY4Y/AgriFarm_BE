using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.FarmSeeds
{
    public class UpdatePropertyCommand: IRequest<SeedResponse>
    {
        public Guid Id { get; set; }
        public List<PropertyValue> Properties { get; set; } = new();
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, SeedResponse>
    {
        private ISQLRepository<SeedlingContext, FarmSeed> _seeds;
        private ISQLRepository<SeedlingContext, ComponentProperty> _properties;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdatePropertyCommandHandler> _logger;

        public UpdatePropertyCommandHandler(ISQLRepository<SeedlingContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<UpdatePropertyCommandHandler> logger,
            IUnitOfWork<SeedlingContext> unit,
            ISQLRepository<SeedlingContext, ComponentProperty> properties)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _properties = properties;
        }

        public async Task<SeedResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id,
                                    ls => ls.Include(x=>x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            var old = item.Properties;
            await _properties.RawDeleteBatchAsync(old);

            item.Properties = _mapper.Map<List<ComponentProperty>>(request.Properties);

            await _properties.AddBatchAsync(item.Properties);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
