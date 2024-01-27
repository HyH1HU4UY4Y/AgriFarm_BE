using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands
{
    public class CreateNewSupplierCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }

    public class CreateNewSupplierCommandHandler : IRequestHandler<CreateNewSupplierCommand, Guid>
    {
        private ISQLRepository<SupplyContext, Supplier> _supplies;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<CreateNewSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public CreateNewSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> supplies,
            IUnitOfWork<SupplyContext> unit,
            ILogger<CreateNewSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _supplies = supplies;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNewSupplierCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Supplier>(request);
            item = await _supplies.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);


            return item.Id;
        }
    }


}
