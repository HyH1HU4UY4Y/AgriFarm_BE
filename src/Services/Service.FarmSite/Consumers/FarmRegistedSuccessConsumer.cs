using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Messages;
using Infrastructure.FarmSite.Contexts;
using MassTransit;
using MediatR;
using Service.FarmSite.Commands;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Consumers
{
    public class FarmRegistedSuccessConsumer : IConsumer<IntegrationEventMessage<FarmRegistration>>
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

        public async Task Consume(ConsumeContext<IntegrationEventMessage<FarmRegistration>> context)
        {
            var form = context.Message.Data;
            var siteCmd = new CreateNewFarmCommand
            {
                Name = form.SiteName,
                SiteKey = form.SiteKey,
            };

            var siteId = await _mediator.Send(siteCmd);

            var billCmd = new CreateSubscriptBillCommand
            {
                SolutionId = form.SolutionId,
                SiteId = siteId,
                Price = form.Solution.Price,
                StartIn = DateTime.Now,
                EndIn = DateTime.Now.AddMonths((int)(form.Solution!.DurationInMonth ?? 1)),
            };

            await _mediator.Send(billCmd);

            var capCmd = new AddCapitalStateCommand
            {
                Amount = (double)-(form.Solution.Price),
                Description = $"Start new farm service cost at {DateTime.Now.ToShortDateString()}",
                SiteId = siteId
            };

            await _mediator.Send(capCmd);

            await _bus.SendToEndpoint(new IntegrationEventMessage<InitFarmOwnerEvent>(
                new InitFarmOwnerEvent(
                    siteId: siteId,
                    fullName: form.Name,
                    userName: form.Email,
                    email: form.Email,
                    address: form.Address
                ), EventState.Add), EventQueue.InitFarmOwner);
        }
    }
}
