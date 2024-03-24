using AutoMapper;
using EventBus.Events.Messages;
using Infrastructure.Common.Replication.Commands;
using MassTransit;
using MediatR;
using SharedDomain.Entities.Users;

namespace Service.FarmScheduling.Consumers.Replication
{
    public class UserReplicatedConsumer : IConsumer<IntegrationEventMessage<MinimalUserInfo>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private ILogger<UserReplicatedConsumer> _logger;

        public UserReplicatedConsumer(IMapper mapper,
            IMediator mediator,
            ILogger<UserReplicatedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IntegrationEventMessage<MinimalUserInfo>> context)
        {
            var detail = context.Message.Data;


            await _mediator.Send(new SaveEntityCommand<MinimalUserInfo>
            {
                Item = detail,
                State = context.Message.State
            });
        }
    }
}
