using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
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
    public class UpdateFarmFertilizeCommand : IRequest<FertilizeResponse>
    {
        public Guid Id { get; set; }
        public FertilizeInfoRequest Fertilize { get; set; }
    }

    public class UpdateFarmFertilizeCommandHandler : IRequestHandler<UpdateFarmFertilizeCommand, FertilizeResponse>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmFertilizeCommandHandler> _logger;
        private IBus _bus;

        public UpdateFarmFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<UpdateFarmFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit,
            IBus bus)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
            _bus = bus;
        }

        public async Task<FertilizeResponse> Handle(UpdateFarmFertilizeCommand request, CancellationToken cancellationToken)
        {
            var item = await _fertilizes.GetOne(e => e.Id == request.Id,
                                ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Fertilize, item);

            await _fertilizes.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            await _bus.ReplicateFertilize(item, EventState.Modify);

            return _mapper.Map<FertilizeResponse>(item);
        }
    }
}
