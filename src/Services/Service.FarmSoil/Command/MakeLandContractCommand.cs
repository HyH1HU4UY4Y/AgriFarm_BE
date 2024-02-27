using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.Soil.Contexts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Soil.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Command
{
    public class MakeLandContractCommand : IRequest<LandResponse>
    {
        public Guid Id { get; set; }
        public SupplyContractRequest Details { get; set; }
        public Guid SiteId { get; set; }
    }

    public class MakeLandContractCommandHandler : IRequestHandler<MakeLandContractCommand, LandResponse>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private IMapper _mapper;
        private ILogger<MakeLandContractCommandHandler> _logger;
        private IBus _bus;

        public MakeLandContractCommandHandler(ISQLRepository<FarmSoilContext, FarmSoil> pesticides,
            IMapper mapper,
            ILogger<MakeLandContractCommandHandler> logger,
            IUnitOfWork<FarmSoilContext> unit,
            IBus bus)
        {
            _lands = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<LandResponse> Handle(MakeLandContractCommand request, CancellationToken cancellationToken)
        {

            var item = await _lands.GetOne(e => e.Id == request.Id
                                                    && e.SiteId == request.SiteId
                                                );

            if (item == null)
            {
                throw new NotFoundException();
            }


            item.ExpiredIn = request.Details.ValidTo;


            await _lands.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.SendToEndpoint<IntegrationEventMessage<SupplyingEvent>>(
                new(
                    new()
                    {
                        SiteId = request.SiteId,
                        Quanlity = item.Acreage,
                        UnitPrice = request.Details.Price,
                        Unit = item.Unit??"m2",
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
                EventQueue.LandSupplyingQueue
            );

            return _mapper.Map<LandResponse>(item);
        }
    }
}
