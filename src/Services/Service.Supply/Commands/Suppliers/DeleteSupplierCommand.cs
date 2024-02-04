using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Suppliers
{
    public class DeleteSupplierCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private ISQLRepository<SupplyContext, Supplier> _suppliers;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<DeleteSupplierCommandHandler> _logger;
        private IMapper _mapper;

        public DeleteSupplierCommandHandler(ISQLRepository<SupplyContext, Supplier> suppliers,
            IUnitOfWork<SupplyContext> unit,
            ILogger<DeleteSupplierCommandHandler> logger,
            IMapper mapper)
        {
            _suppliers = suppliers;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var item = await _suppliers.GetOne(e => e.Id == request.Id);
            if (item == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(request, item);
            await _suppliers.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }

    }
}
