using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.FarmSite.Contexts;
using MassTransit;
using MediatR;
using Service.FarmSite.Commands;
using Service.FarmSite.Commands.Farms;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Consumers
{
    public class FarmRegistedSuccessConsumer : IConsumer<IntegrationEventMessage<AcceptFarmRegistEvent>>
    {
        
        
        private IMediator _mediator;
        private ILogger<FarmRegistedSuccessConsumer> _logger;
        private IBus _bus;

        public FarmRegistedSuccessConsumer(
            IMediator mediator,
            ILogger<FarmRegistedSuccessConsumer> logger,
            IBus bus)
        {
            _mediator = mediator;
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<AcceptFarmRegistEvent>> context)
        {
            var form = context.Message.Data;
            var siteCmd = new CreateNewFarmCommand
            {
                Site = new()
                {
                    Name = form.SiteName,
                    SiteCode = form.SiteCode,
                    Address = form.Address
                },
                
                IsActive = true
            };

            var siteId = await _mediator.Send(siteCmd);

            var billCmd = new CreateSubscriptBillCommand
            {
                SolutionId = form.SolutionId,
                SiteId = siteId,
                Price = form.Cost,
                StartIn = DateTime.Now,
                EndIn = DateTime.Now.AddMonths((int)(form.DurationInMonth ?? 1)),
            };

            await _mediator.Send(billCmd);

            var capCmd = new AddCapitalStateCommand
            {
                Amount = (double)-(form.Cost),
                Description = $"Start new farm service cost at {DateTime.Now.ToShortDateString()}",
                SiteId = siteId
            };

            await _mediator.Send(capCmd);

            await _bus.SendToEndpoint(new IntegrationEventMessage<InitFarmOwnerEvent>(
                new InitFarmOwnerEvent(
                    siteId: siteId,
                    siteName: form.SiteName,
                    siteCode: form.SiteCode,
                    firstName: form.FirstName,
                    lastName: form.LastName,
                    userName: form.Email,
                    email: form.Email,
                    address: form.Address??"",
                    phoneNumber: form.Phone??""
                ), EventState.Add), EventQueue.InitFarmOwnerQueue);
        }
    }
}
