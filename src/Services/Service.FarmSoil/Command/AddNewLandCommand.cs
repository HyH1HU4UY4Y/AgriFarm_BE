using AutoMapper;
using EventBus;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Events.Messages;
using EventBus.Utils;
using Infrastructure.Soil.Contexts;
using MassTransit;
using MediatR;
using Service.Soil.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Command
{
    public class AddNewLandCommand: IRequest<LandResponse>
    {
        public Guid SiteId { get; set; }
        public LandRequest Land { get; set; }
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

            await _bus.ReplicateSoil(item, EventState.Add);            

            return _mapper.Map<LandResponse>(item);
        }
    }


}
