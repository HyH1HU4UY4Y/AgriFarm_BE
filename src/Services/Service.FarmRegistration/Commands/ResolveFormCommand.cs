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
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Users;

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
        private readonly ISQLRepository<RegistrationContext, Site> _sites;
        private readonly ISQLRepository<RegistrationContext, MinimalUserInfo> _users;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ResolveFormCommandHandler> _logger;
        private readonly IBus _bus;

        public ResolveFormCommandHandler(IMapper mapper,
            IUnitOfWork<RegistrationContext> unitOfWork,
            ISQLRepository<RegistrationContext, FarmRegistration> registrations,
            ILogger<ResolveFormCommandHandler> logger,
            IBus bus,
            ISQLRepository<RegistrationContext, Site> sites,
            ISQLRepository<RegistrationContext, MinimalUserInfo> users)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _registrations = registrations;
            _logger = logger;
            _bus = bus;
            _sites = sites;
            _users = users;
        }

        public async Task<Guid> Handle(ResolveFormCommand request, CancellationToken cancellationToken)
        {
            string msg = string.Empty;
            //_logger.LogInformation("Starting resolve form...");
            var item = await _registrations.GetOne(e=>e.Id == request.Id && e.IsApprove == DecisonOption.Waiting);
            if (item == null)
            {
                throw new NotFoundException("Not found");
            }
            var site = _sites.GetOne(e => e.Name == item.SiteName).Result;
            var user = _users.GetOne(e => e.UserName == item.Email).Result;
            if (site?.Name == item.SiteName) msg += "Farm Name already exists.";
            if (site?.SiteCode == item.SiteCode) msg += " Farm Code already exists.";
            if (user?.UserName == item.Email) msg += " Email already exists.";

            if (!string.IsNullOrEmpty(msg.Trim()))
            {
                throw new BadRequestException(msg.Trim());
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
