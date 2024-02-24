using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using Service.Water.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Water.Queries
{
    public class GetFarmWaterByIdQuery : IRequest<WaterResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetFarmWaterByIdQueryHandler : IRequestHandler<GetFarmWaterByIdQuery, WaterResponse>
    {
        private ISQLRepository<FarmWaterContext, FarmWater> _water;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmWaterByIdQueryHandler> _logger;

        public GetFarmWaterByIdQueryHandler(ISQLRepository<FarmWaterContext, FarmWater> water,
            IMapper mapper,
            ILogger<GetFarmWaterByIdQueryHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _water = water;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }


        public async Task<WaterResponse> Handle(GetFarmWaterByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _water.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist!");
            }


            return _mapper.Map<WaterResponse>(item);
        }
    }
}
