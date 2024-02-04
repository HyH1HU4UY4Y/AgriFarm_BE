using AutoMapper;
using Infrastructure.Water.Contexts;
using MediatR;
using Service.Water.DTOs;
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
        private ISQLRepository<FarmWaterContext, ReferencedSeed> _seeds;
        private IUnitOfWork<FarmWaterContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmWaterByIdQueryHandler> _logger;

        public GetFarmWaterByIdQueryHandler(ISQLRepository<FarmWaterContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<GetFarmWaterByIdQueryHandler> logger,
            IUnitOfWork<FarmWaterContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }


        public async Task<WaterResponse> Handle(GetFarmWaterByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _seeds.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist!");
            }


            return _mapper.Map<WaterResponse>(item);
        }
    }
}
