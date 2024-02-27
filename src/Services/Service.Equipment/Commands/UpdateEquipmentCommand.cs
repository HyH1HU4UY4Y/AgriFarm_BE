using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Equipment.Contexts;
using MassTransit;
using MediatR;
using Service.Equipment.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Commands
{
    public class UpdateEquipmentCommand : IRequest<EquipmentResponse>
    {
        public Guid Id { get; set; }
        public EquipmentRequest Equipment { get; set; }

    }

    public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, EquipmentResponse>
    {
        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateEquipmentCommandHandler> _logger;
        private IBus _bus;

        public UpdateEquipmentCommandHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<UpdateEquipmentCommandHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit,
            IBus bus)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<EquipmentResponse> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var item = await _equipments.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Equipment, item);

            await _equipments.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateEquipment(item, EventState.Modify);

            return _mapper.Map<EquipmentResponse>(item);
        }
    }
}
