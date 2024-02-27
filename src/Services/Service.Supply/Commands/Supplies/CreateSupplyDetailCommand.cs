using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Service.Supply.DTOs;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Commands.Supplies
{
    public class CreateSupplyDetailCommand : IRequest<Guid>
    {
        public Guid SiteId { get; set; }
        public NewSupplyRequest Detail { get; set; }
    }

    public class AddNewContractCommandHandler : IRequestHandler<CreateSupplyDetailCommand, Guid>
    {
        private ISQLRepository<SupplyContext, SupplyDetail> _details;
        private ISQLRepository<SupplyContext, BaseComponent> _components;
        private IMapper _mapper;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<AddNewContractCommandHandler> _logger;

        public AddNewContractCommandHandler(ISQLRepository<SupplyContext, SupplyDetail> details,
            IMapper mapper,
            IUnitOfWork<SupplyContext> unit,
            ILogger<AddNewContractCommandHandler> logger,
            ISQLRepository<SupplyContext, BaseComponent> components)
        {
            _details = details;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _components = components;
        }

        public async Task<Guid> Handle(CreateSupplyDetailCommand request, CancellationToken cancellationToken)
        {
            
            var item = _mapper.Map<SupplyDetail>(request.Detail);

            item.SiteId = request.SiteId;

            var rs = await _details.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);


            return rs.Id;
        }
    }
}
