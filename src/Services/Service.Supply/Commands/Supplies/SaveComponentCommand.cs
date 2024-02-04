using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.Commands.Suppliers;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Supplies
{
    public class SaveComponentCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public Guid SiteId { get; set; }
        public string Name { get; set; }
        public bool IsConsumable { get; set; }
    }

    public class SaveComponentCommandHandler : IRequestHandler<SaveComponentCommand, Guid>
    {

        private ISQLRepository<SupplyContext, BaseComponent> _components;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<SaveComponentCommandHandler> _logger;
        private IMapper _mapper;

        public SaveComponentCommandHandler(ISQLRepository<SupplyContext, BaseComponent> components,
            IUnitOfWork<SupplyContext> unit,
            ILogger<SaveComponentCommandHandler> logger,
            IMapper mapper)
        {
            _components = components;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveComponentCommand request, CancellationToken cancellationToken)
        {

            var item = await _components.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                item = await _components.AddAsync(new()
                {
                    Name = request.Name,
                    IsConsumable = request.IsConsumable,
                    SiteId = request.SiteId,
                });
                await _unit.SaveChangesAsync(cancellationToken);
                return item.Id;
            }

            return item.Id;
        }
    }
}
