using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Water.Commands
{
    public class RemoveFarmWaterCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveFarmWaterCommandHandler : IRequestHandler<RemoveFarmWaterCommand, Guid>
    {
        private ISQLRepository<FarmWaterContext, FarmWater> _waters;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveFarmWaterCommandHandler> _logger;

        public RemoveFarmWaterCommandHandler(ISQLRepository<FarmWaterContext, FarmWater> waters,
            IMapper mapper,
            ILogger<RemoveFarmWaterCommandHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _waters = waters;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveFarmWaterCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _waters.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _waters.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
