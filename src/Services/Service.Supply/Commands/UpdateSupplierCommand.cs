using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands
{
    public class UpdateSupplierCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Guid>
    {
        private ISQLRepository<SupplyContext, Supplier> _suppliers;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<UpdateSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public UpdateSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<UpdateSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _suppliers = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var item = await _suppliers.GetOne(e => e.Id == request.Id);
            if(item == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request, item);
            await _suppliers.UpdateAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);


            return item.Id;
        }
    }

}
