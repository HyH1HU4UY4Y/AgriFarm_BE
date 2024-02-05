using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using Service.Water.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Water.Commands
{
    public class AddFarmWaterCommand : IRequest<Guid>
    {
        public WaterRequest Water { get; set; }
    }

    public class AddFarmWaterCommandHandler : IRequestHandler<AddFarmWaterCommand, Guid>
    {
        private ISQLRepository<FarmWaterContext, FarmWater> _waters;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<AddFarmWaterCommandHandler> _logger;

        public AddFarmWaterCommandHandler(ISQLRepository<FarmWaterContext, FarmWater> waters,
            IMapper mapper,
            ILogger<AddFarmWaterCommandHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _waters = waters;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddFarmWaterCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with ref Water info
            */

            var item = _mapper.Map<FarmWater>(request.Water);

            await _waters.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
