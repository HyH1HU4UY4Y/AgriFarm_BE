using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.Fertilize.Contexts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.FarmFertilizes
{
    public class SupplyFertilizeCommand : IRequest<FertilizeResponse>
    {
        public Guid Id { get; set; }
        public SupplyRequest Details { get; set; }
        public Guid SiteId { get; set; }
    }

    public class SupplyFertilizeCommandHandler : IRequestHandler<SupplyFertilizeCommand, FertilizeResponse>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<SupplyFertilizeCommandHandler> _logger;
        private IBus _bus;

        public SupplyFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<SupplyFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit,
            IBus bus)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<FertilizeResponse> Handle(SupplyFertilizeCommand request, CancellationToken cancellationToken)
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
                EventQueue.FertilizeSupplyingQueue
            );

            return _mapper.Map<FertilizeResponse>(item);
        }
    }
}
