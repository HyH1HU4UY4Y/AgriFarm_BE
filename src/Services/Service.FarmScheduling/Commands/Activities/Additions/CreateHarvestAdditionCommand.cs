using MediatR;

namespace Service.FarmScheduling.Commands.Activities.Additions
{
    public class CreateHarvestAdditionEvent: INotification
    {
        public Guid ActivityId { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }

    public class CreateHarvestAdditionEventHandler : INotificationHandler<CreateHarvestAdditionEvent>
    {
        public Task Handle(CreateHarvestAdditionEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
