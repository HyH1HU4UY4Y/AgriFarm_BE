using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Soil.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Command
{
    public class DeleteLandCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLandCommandHandler : IRequestHandler<DeleteLandCommand, Guid>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<DeleteLandCommandHandler> _logger;
        private IMapper _mapper;
        private IBus _bus;

        public DeleteLandCommandHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<DeleteLandCommandHandler> logger,
            IMapper mapper,
            IBus bus)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<Guid> Handle(DeleteLandCommand request, CancellationToken cancellationToken)
        {

            var item = await _lands.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Land is not exist");
            }
            
            await _lands.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateSoil(item, EventState.SoftDelete);

            return item.Id;
        }
    }
}
