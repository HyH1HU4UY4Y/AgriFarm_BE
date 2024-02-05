using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Suppliers
{
    public class CreateNewSupplierCommand : IRequest<Guid>
    {
        public SupplierRequest Supplier {  get; set; }
        public Guid? SiteId { get; set; }
    }

    public class CreateNewSupplierCommandHandler : IRequestHandler<CreateNewSupplierCommand, Guid>
    {
        private ISQLRepository<SupplyContext, Supplier> _supplies;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<CreateNewSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public CreateNewSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<CreateNewSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _supplies = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNewSupplierCommand request, CancellationToken cancellationToken)
        {
            if(request.SiteId == null)
            {
                //TODO: cactch the site from header
            }

            var item = await _supplies
                .GetOne(e => string.Equals(e.Name, request.Supplier.Name) 
                    //&& (!string.IsNullOrWhiteSpace(e.Address) && !string.IsNullOrWhiteSpace(request.Address))
                     && e.CreatedByFarmId == request.SiteId
                     && e.Address!.Contains(request.Supplier.Address!));

            if ( item == null)
            {
                item = _mapper.Map<Supplier>(request.Supplier);
                item.CreatedByFarmId = request.SiteId;
                item = await _supplies.AddAsync(item);
                await _unit.SaveChangesAsync(cancellationToken);
            }

            return item.Id;
        }
    }


}
