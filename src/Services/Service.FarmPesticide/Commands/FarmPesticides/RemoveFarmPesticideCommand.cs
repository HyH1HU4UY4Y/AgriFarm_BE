using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Pesticide.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class RemoveFarmPesticideCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveFarmPesticideCommandHandler : IRequestHandler<RemoveFarmPesticideCommand, Guid>
    {
        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveFarmPesticideCommandHandler> _logger;
        private IBus _bus;

        public RemoveFarmPesticideCommandHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<RemoveFarmPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit,
            IBus bus)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<Guid> Handle(RemoveFarmPesticideCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _pesticides.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _pesticides.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);


            await _bus.ReplicatePesticide(item, EventState.SoftDelete);

            return item.Id;
        }
    }
}
