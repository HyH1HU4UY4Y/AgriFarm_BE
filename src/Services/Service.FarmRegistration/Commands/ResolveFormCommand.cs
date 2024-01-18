using SharedApplication.CQRS;
using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using SharedDomain.Defaults;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;
using SharedDomain.Exceptions;
using MassTransit;
using EventBus;
using EventBus.Messages;
using EventBus.Defaults;
using EventBus.Events;

namespace Service.FarmRegistry.Commands
{
    public record ResolveFormCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DecisonOption Decison { get; set; }
        [StringLength(3000)]
        public string Notes { get; set; }
    }

    public class ResolveFormCommandHandler : IRequestHandler<ResolveFormCommand, Guid>
    {
        private readonly ISQLRepository<RegistrationContext, FarmRegistration> _registrations;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ResolveFormCommandHandler> _logger;
        private readonly IBus _bus;

        public ResolveFormCommandHandler(IMapper mapper,
            IUnitOfWork<RegistrationContext> unitOfWork,
            ISQLRepository<RegistrationContext, FarmRegistration> registrations,
            ILogger<ResolveFormCommandHandler> logger,
            IBus bus)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _registrations = registrations;
            _logger = logger;
            _bus = bus;
        }

        public async Task<Guid> Handle(ResolveFormCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting resolve form...");
            var item = await _registrations.GetOne(e=>e.Id == request.Id && e.IsApprove == DecisonOption.Waiting);
            if (item == null)
            {
                throw new NotFoundException("Not found");
            }

            item.IsApprove = request.Decison;
            await _registrations.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            await _bus.SendToEndpoint(new IntegrationEventMessage<AcceptFarmRegistEvent>(
                _mapper.Map<AcceptFarmRegistEvent>(item),
                EventState.Add
            ), EventQueue.RegistFarmQueue);

            return item.Id;
        }
    }
}
