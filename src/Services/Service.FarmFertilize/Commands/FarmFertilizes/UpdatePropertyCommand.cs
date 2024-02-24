using AutoMapper;
using MediatR;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using Service.Fertilize.DTOs;
using Infrastructure.Fertilize.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Service.Fertilize.Commands.FarmFertilizes
{
    public class UpdatePropertyCommand : IRequest<FertilizeResponse>
    {
        public Guid FertilizeId { get; set; }
        public List<PropertyValue> Properties { get; set; }

    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, FertilizeResponse>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private ISQLRepository<FarmFertilizeContext, ComponentProperty> _properties;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdatePropertyCommandHandler> _logger;

        public UpdatePropertyCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<UpdatePropertyCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit,
            ISQLRepository<FarmFertilizeContext, ComponentProperty> properties)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _properties = properties;
        }

        public async Task<FertilizeResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var item = await _fertilizes.GetOne(e => e.Id == request.FertilizeId,
                                    ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            var old = item.Properties;
            await _properties.RawDeleteBatchAsync(old);

            item.Properties = _mapper.Map<List<ComponentProperty>>(request.Properties);

            await _properties.AddBatchAsync(item.Properties);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FertilizeResponse>(item);
        }
    }
}
