using AutoMapper;
using EventBus.Events;
using EventBus.Events.Messages;
using MassTransit;
using MediatR;
using Service.Supply.Commands.Suppliers;
using Service.Supply.Commands.Supplies;

namespace Service.Supply.Consumers.Supplying
{
    public class SeedSupplyingConsumer : IConsumer<IntegrationEventMessage<SupplyingEvent>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<SeedSupplyingConsumer> _logger;

        public SeedSupplyingConsumer(IMapper mapper,
            IMediator mediator, 
            ILogger<SeedSupplyingConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<SupplyingEvent>> context)
        {
            var detail = context.Message.Data;

            var supplier = await _mediator.Send(new SaveSupplierCommand
            {
                SiteId = detail.SiteId,
                Supplier = new()
                {
                    Address = detail.Supplier.Address,
                    Name = detail.Supplier.Name,
                }
            });

            await _mediator.Send(new CreateSupplyDetailCommand
            {
                Detail = new()
                {
                    ComponentId = detail.Item.Id,
                    Content = $"Add {detail.Quanlity} ({detail.Unit}) {detail.Item.Name}. With content: \"{detail.Content?.Trim()}\"".Trim(),
                    Quantity = detail.Quanlity,
                    Unit = detail.Unit,
                    SupplierId = supplier.Id,
                    UnitPrice = detail.UnitPrice,
                    ValidFrom = detail.ValidFrom,
                    ValidTo = detail.ValidTo,
                    
                },
                SiteId = detail.SiteId,
            });
        }
    }
}
