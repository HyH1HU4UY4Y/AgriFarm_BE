using EventBus.Events;
using EventBus.Messages;
using Infrastructure.Identity.Contexts;
using MassTransit;
using MediatR;
using Service.Identity.Commands.Users;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Consumers
{
    public class InitFarmOwnerConsumer : IConsumer<IntegrationEventMessage<InitFarmOwnerEvent>>
    {
        private IMediator _mediator;
        private ILogger<InitFarmOwnerConsumer> _logger;
        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;

        public InitFarmOwnerConsumer(IMediator mediator,
            ILogger<InitFarmOwnerConsumer> logger,
            ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _sites = sites;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<InitFarmOwnerEvent>> context)
        {
            
            var data = context.Message.Data;

            await _sites.AddAsync(new()
            {
                Id = data.SiteId,
                SiteCode = data.SiteCode,
                Name = data.SiteName
            });

            await _unitOfWork.SaveChangesAsync();

            var createCmd = new CreateMemberCommand
            {
                Staff = new()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Password = "@123456",
                    UserName = data.Email
                },
                
                SiteId = data.SiteId,
                AccountType = AccountType.Admin
            };

            var rs = await _mediator.Send(createCmd);

        }
    }
}
