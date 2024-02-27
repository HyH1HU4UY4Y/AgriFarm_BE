using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Equipment.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Commands
{
    public class RemoveEquipmentCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveEquipmentCommandHandler : IRequestHandler<RemoveEquipmentCommand, Guid>
    {
        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveEquipmentCommandHandler> _logger;
        private IBus _bus;

        public RemoveEquipmentCommandHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<RemoveEquipmentCommandHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit,
            IBus bus)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<Guid> Handle(RemoveEquipmentCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _equipments.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _equipments.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);
            await _bus.ReplicateEquipment(item, EventState.SoftDelete);

            return item.Id;
        }
    }
}
