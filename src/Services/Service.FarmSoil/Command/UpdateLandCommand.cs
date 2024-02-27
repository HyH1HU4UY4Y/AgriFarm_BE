using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
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
    public class UpdateLandCommand: IRequest<LandResponse>
    {
        public Guid Id { get; set; }
        public LandRequest Land { get; set; }
    }

    public class UpdateLandCommandHandler : IRequestHandler<UpdateLandCommand, LandResponse>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<UpdateLandCommandHandler> _logger;
        private IMapper _mapper;
        private IBus _bus;

        public UpdateLandCommandHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<UpdateLandCommandHandler> logger,
            IMapper mapper,
            IBus bus)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<LandResponse> Handle(UpdateLandCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _lands.GetOne(e=>e.Id == request.Id,
                                            ls => ls.Include(x=>x.Site)
                                            );

            if(item == null)
            {
                throw new NotFoundException("Land not exist");
            }

            _mapper.Map(request.Land, item);
            await _lands.UpdateAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateSoil(item, EventState.Modify);

            return _mapper.Map<LandResponse>(item);
        }
    }

}
