using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Supplies
{
    public class DeleteSupplyContractCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSupplyContractCommandHandler : IRequestHandler<DeleteSupplyContractCommand, Guid>
    {

        private ISQLRepository<SupplyContext, SupplyDetail> _details;
        private ISQLRepository<SupplyContext, BaseComponent> _components;
        private IMapper _mapper;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<DeleteSupplyContractCommandHandler> _logger;

        public DeleteSupplyContractCommandHandler(ISQLRepository<SupplyContext, SupplyDetail> details,
            IMapper mapper,
            IUnitOfWork<SupplyContext> unit,
            ILogger<DeleteSupplyContractCommandHandler> logger,
            ISQLRepository<SupplyContext, BaseComponent> components)
        {
            _details = details;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _components = components;
        }

        public async Task<Guid> Handle(DeleteSupplyContractCommand request, CancellationToken cancellationToken)
        {
            var item = await _details.GetOne(e=>e.Id == request.Id);
            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return item.Id;
        }
    }
}
