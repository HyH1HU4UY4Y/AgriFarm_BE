using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.Seed.Contexts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Commands.FarmSeeds
{
    public class SupplySeedCommand : IRequest<SeedResponse>
    {
        public Guid Id { get; set; }
        public SupplyRequest Details { get; set; }
        public Guid SiteId { get; set; }
    }

    public class SupplySeedCommandHandler : IRequestHandler<SupplySeedCommand, SeedResponse>
    {
        private ISQLRepository<FarmSeedContext, FarmSeed> _fertilizes;
        private IUnitOfWork<FarmSeedContext> _unit;
        private IMapper _mapper;
        private ILogger<SupplySeedCommandHandler> _logger;
        private IBus _bus;

        public SupplySeedCommandHandler(ISQLRepository<FarmSeedContext, FarmSeed> fertilizes,
            IMapper mapper,
            ILogger<SupplySeedCommandHandler> logger,
            IUnitOfWork<FarmSeedContext> unit,
            IBus bus)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<SeedResponse> Handle(SupplySeedCommand request, CancellationToken cancellationToken)
        {

            var item = await _fertilizes.GetOne(e => e.Id == request.Id
                                                    && e.SiteId == request.SiteId,
                                                ls => ls.Include(x => x.Properties)
                                                );

            if (item == null)
            {
                throw new NotFoundException();
            }


            /*TODO:
                - process for many kind of measure units
                
            */

            item.Stock += request.Details.Quanlity;
            item.UnitPrice = request.Details.UnitPrice;

            //alt: default unit with request unit 
            //item.Unit = request.Details.Unit;

            await _fertilizes.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.SendToEndpoint<IntegrationEventMessage<SupplyingEvent>>(
                new(
                    new()
                    {
                        SiteId = request.SiteId,
                        Quanlity = request.Details.Quanlity,
                        UnitPrice = request.Details.UnitPrice,
                        Unit = request.Details.Unit,
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
                        Content = request.Details.Content
                    },
                    EventState.None
                ),
                EventQueue.SeedSupplyingQueue
            );

            return _mapper.Map<SeedResponse>(item);
        }
    }
}
