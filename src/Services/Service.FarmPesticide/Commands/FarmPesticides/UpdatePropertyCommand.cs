using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class UpdatePropertyCommand: IRequest<PesticideResponse>
    {
        public Guid PesticideId {  get; set; }
        public List<PropertyValue> Properties { get; set; }

    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PesticideResponse>
    {
        private ISQLRepository<FarmPesticideContext, FarmPesticide> _ppps;
        private ISQLRepository<FarmPesticideContext, ComponentProperty> _properties;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdatePropertyCommandHandler> _logger;

        public UpdatePropertyCommandHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> ppps,
            IMapper mapper,
            ILogger<UpdatePropertyCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit,
            ISQLRepository<FarmPesticideContext, ComponentProperty> properties)
        {
            _ppps = ppps;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _properties = properties;
        }

        public async Task<PesticideResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var item = await _ppps.GetOne(e => e.Id == request.PesticideId,
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

            return _mapper.Map<PesticideResponse>(item);
        }
    }
}
