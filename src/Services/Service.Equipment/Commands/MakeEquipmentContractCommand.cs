using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.Equipment.Contexts;
using MassTransit;
using MediatR;
using Service.Equipment.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Commands
{
    public class MakeEquipmentContractCommand : IRequest<EquipmentResponse>
    {
        public Guid Id { get; set; }
        public SupplyContractRequest Details { get; set; }
        public Guid SiteId { get; set; }
    }

    public class MakeEquipmentContractCommandHandler : IRequestHandler<MakeEquipmentContractCommand, EquipmentResponse>
    {
        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _lands;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<MakeEquipmentContractCommandHandler> _logger;
        private IBus _bus;

        public MakeEquipmentContractCommandHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> pesticides,
            IMapper mapper,
            ILogger<MakeEquipmentContractCommandHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit,
            IBus bus)
        {
            _lands = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<EquipmentResponse> Handle(MakeEquipmentContractCommand request, CancellationToken cancellationToken)
        {

            var item = await _lands.GetOne(e => e.Id == request.Id
                                                    && e.SiteId == request.SiteId
                                                );

            if (item == null)
            {
                throw new NotFoundException();
            }


            item.ExpiredIn = request.Details.ValidTo;
            item.UnitPrice = request.Details.Price;


            await _lands.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.SendToEndpoint<IntegrationEventMessage<SupplyingEvent>>(
                new(
                    new()
                    {
                        SiteId = request.SiteId,
                        Quanlity = 1,
                        UnitPrice = request.Details.Price,
                        Unit = item.Unit,
                        Item = new()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Type = item.GetType(),
                        },
                        Supplier = new()
                        {
                            Id = request.Details.Supplier.Id ?? Guid.Empty,
                            Name = request.Details.Supplier.Name,
                            Address = request.Details.Supplier.Address
                        },
                        Content = request.Details.Content,
                        ValidFrom = request.Details.ValidFrom,
                        ValidTo = request.Details.ValidTo,
                    },
                    EventState.None
                ),
                EventQueue.EquipmentSupplyingQueue
            );

            return _mapper.Map<EquipmentResponse>(item);
        }
    }
}
