using AutoMapper;
using EventBus.Events;
using EventBus.Messages;
using MassTransit;
using MediatR;
using Service.Supply.Commands.Suppliers;
using Service.Supply.Commands.Supplies;

namespace Service.Supply.Consumers
{
    public class NewSupplyContractConsumer : IConsumer<IntegrationEventMessage<NewSupplyContractEvent>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<NewSupplyContractConsumer> _logger;

        public NewSupplyContractConsumer(IMapper mapper, 
            IMediator mediator, ILogger<NewSupplyContractConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<NewSupplyContractEvent>> context)
        {
            var detail = context.Message.Data;

            Guid supplierId = Guid.Empty;

            if (detail.SupplierId == null)
            {
                supplierId = await _mediator.Send(new CreateNewSupplierCommand
                {
                    
                    SiteId = detail.SiteId,
                    Supplier = new()
                    {
                        Name= detail.SupplierName,
                    }
                });
            }

            var componentId = await _mediator.Send(new SaveComponentCommand
            {
                Id = detail.ComponentId,
                IsConsumable = detail.IsConsumable,
                Name = detail.ComponentName,
                SiteId = detail.SiteId,
            });


            var rs = await _mediator.Send(new AddNewContractCommand()
            {
                Contract = new()
                {
                    Content = detail.Content,
                    Resource = detail.Resource,
                    IsLimitTime = detail.IsLimitTime,
                    ExpiredIn = detail.ExpiredIn,
                    Quantity = detail.Quantity,
                    Unit = detail.Unit,
                    UnitPrice = detail.UnitPrice,
                    ComponentId = componentId,
                    SupplierId = supplierId
                },

                SiteId = detail.SiteId,
                
            });




            await Task.CompletedTask;
        }
    }
}
