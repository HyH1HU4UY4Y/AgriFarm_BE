using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Fertilize.Contexts;
using MassTransit;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.FarmFertilizes
{
    public class RemoveFarmFertilizeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveFarmFertilizeCommandHandler : IRequestHandler<RemoveFarmFertilizeCommand, Guid>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveFarmFertilizeCommandHandler> _logger;
        private IBus _bus;

        public RemoveFarmFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<RemoveFarmFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit,
            IBus bus)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<Guid> Handle(RemoveFarmFertilizeCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _fertilizes.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _fertilizes.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);


            await _bus.ReplicateFertilize(item, EventState.SoftDelete);

            return item.Id;
        }
    }
}
