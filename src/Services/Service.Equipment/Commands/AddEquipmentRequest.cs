using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Equipment.Contexts;
using MassTransit;
using MediatR;
using Service.Equipment.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Commands
{
    public class AddEquipmentCommand: IRequest<EquipmentResponse>
    {
        public EquipmentRequest Equipment {  get; set; }
        public Guid SiteId { get; set; }

    }

    public class AddEquipmentCommandHandler : IRequestHandler<AddEquipmentCommand, EquipmentResponse>
    {
        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<AddEquipmentCommandHandler> _logger;
        private IBus _bus;

        public AddEquipmentCommandHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<AddEquipmentCommandHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit,
            IBus bus)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<EquipmentResponse> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                //- check for super admin
                
            */

            var item = _mapper.Map<FarmEquipment>(request.Equipment);
            item.SiteId = request.SiteId;

            await _equipments.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateEquipment(item, EventState.Add);
            return _mapper.Map<EquipmentResponse>(item);
        }
    }
}
