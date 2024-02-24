using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using Service.Water.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Water.Commands
{
    public class UpdateFarmWaterCommand : IRequest<WaterResponse>
    {
        public Guid Id { get; set; }
        public WaterRequest Water { get; set; }
    }

    public class UpdateFarmWaterCommandHandler : IRequestHandler<UpdateFarmWaterCommand, WaterResponse>
    {
        private ISQLRepository<FarmWaterContext, FarmWater> _waters;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmWaterCommandHandler> _logger;

        public UpdateFarmWaterCommandHandler(ISQLRepository<FarmWaterContext, FarmWater> waters,
            IMapper mapper,
            ILogger<UpdateFarmWaterCommandHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _waters = waters;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<WaterResponse> Handle(UpdateFarmWaterCommand request, CancellationToken cancellationToken)
        {
            var item = await _waters.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Water, item);

            await _waters.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WaterResponse>(item);
        }
    }
}
