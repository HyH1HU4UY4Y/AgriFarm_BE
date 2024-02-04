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
    public class AddNewContractCommand : IRequest<Guid>
    {
        public Guid? SiteId { get; set; }
        public NewContractRequest Contract { get; set; }
    }

    public class AddNewContractCommandHandler : IRequestHandler<AddNewContractCommand, Guid>
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

        public async Task<Guid> Handle(AddNewContractCommand request, CancellationToken cancellationToken)
        {
            

            if (_components.GetOne(e => e.Id == request.Contract.ComponentId) == null)
            {
                throw new BadRequestException("Item not exist.");
            }

            var item = _mapper.Map<SupplyDetail>(request.Contract);

            //TODO: process for farm identity and super admin
            if (request.SiteId == null)
            {
                //TODO: replace here
                item.SiteId = new Guid(TempData.FarmId);
            }

            var rs = await _details.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);


            return rs.Id;
        }
    }
}
