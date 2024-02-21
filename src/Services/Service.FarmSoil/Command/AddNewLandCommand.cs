using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Messages;
using Infrastructure.Soil.Contexts;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using Service.Soil.DTOs;
using Service.Soil.Queries;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Soil.Command
{
    public class AddNewLandCommand: IRequest<LandResponse>
    {
        public Guid SiteId { get; set; }
        public LandRequest Land { get; set; }
        public SupplyContractRequest? SupplyContract { get; set; }
        public List<PositionPoint> Positions { get; set; } = new();
    }


    public class AddNewLandCommandHandler : IRequestHandler<AddNewLandCommand, LandResponse>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<AddNewLandCommandHandler> _logger;
        private IMapper _mapper;
        private IBus _bus;

        public AddNewLandCommandHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<AddNewLandCommandHandler> logger,
            IMapper mapper,
            IBus bus)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<LandResponse> Handle(AddNewLandCommand request, CancellationToken cancellationToken)
        {
            

            var item = _mapper.Map<FarmSoil>(request.Land);
            item.SiteId = request.SiteId;

            if (request.Positions.Any())
            {
                item.Positions = request.Positions;
            }

            await _lands.AddAsync(item);


            await _unit.SaveChangesAsync(cancellationToken);


            if(request.SupplyContract!=null)
            {
                await _bus.SendToEndpoint(new IntegrationEventMessage<NewSupplyContractEvent>(
                    new()
                    {
                        ComponentId = item.Id,
                        ComponentName = item.Name,
                        IsConsumable = item.IsConsumable,
                        IsLimitTime = request.SupplyContract.IsLimitTime,
                        ExpiredIn = request.SupplyContract.ExpiredIn,
                        Quantity = item.Acreage,
                        SiteId = item.SiteId,
                        SupplierId = request.SupplyContract.SupplierId,
                        SupplierName = request.SupplyContract.SupplierName,
                        Unit = request.Land.Unit,
                        UnitPrice = request.SupplyContract.Price,
                        Content = request.SupplyContract.Content,
                        Resource = request.SupplyContract.Resource
                    },
                    EventState.Add
                ), EventQueue.SupplyContractQueue);
            }
            

            return _mapper.Map<LandResponse>(item);
        }
    }


}
