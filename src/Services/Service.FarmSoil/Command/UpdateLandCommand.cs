using AutoMapper;
using EventBus.Defaults;
using EventBus.Events;
using EventBus.Messages;
using Infrastructure.Soil.Contexts;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using SharedDomain.Defaults;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Command
{
    public class UpdateLandCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Acreage { get; set; }
        public string Unit { get; set; }
        public Dictionary<double, double> Positions { get; set; }
    }

    public class UpdateLandCommandHandler : IRequestHandler<UpdateLandCommand, Guid>
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

        public async Task<Guid> Handle(UpdateLandCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _lands.GetOne(e=>e.Id == request.Id);

            if(item == null)
            {
                throw new NotFoundException("Land is not exist");
            }

            _mapper.Map(request, item);
            await _lands.UpdateAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);
            

            return item.Id;
        }
    }

}
