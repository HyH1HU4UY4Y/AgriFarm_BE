using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using Infrastructure.Pesticide.Contexts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class SupplyPesticideCommand :IRequest<PesticideResponse>
    {
        public Guid Id { get; set; }
        public SupplyRequest Details { get; set; }
        public Guid SiteId { get; set; }
    }

    public class SupplyPesticideCommandHandler : IRequestHandler<SupplyPesticideCommand, PesticideResponse>
    {
        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<SupplyPesticideCommandHandler> _logger;
        private IBus _bus;

        public SupplyPesticideCommandHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<SupplyPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit,
            IBus bus)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<PesticideResponse> Handle(SupplyPesticideCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _pesticides.GetOne(e=>e.Id == request.Id
                                                    && e.SiteId == request.SiteId,
                                                ls => ls.Include(x => x.Properties)
                                                );
                
            if( item == null )
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

            await _pesticides.UpdateAsync(item);

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
                            Id = request.Details.Supplier.Id??Guid.Empty,
                            Name = request.Details.Supplier.Name,
                            Address = request.Details.Supplier.Address
                        },
                        Content = request.Details.Content
                    },
                    EventState.None
                ),
                EventQueue.PesticideSupplyingQueue
            );

            return _mapper.Map<PesticideResponse>(item);
        }
    }
}
