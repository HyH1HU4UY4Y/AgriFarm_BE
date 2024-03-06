using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.DTOs.Seasons;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Seasons
{
    public class GetSeasonByIdQuery : IRequest<SeasonDetailResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetSeasonByIdQueryHandler : IRequestHandler<GetSeasonByIdQuery, SeasonDetailResponse>
    {
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<GetSeasonByIdQueryHandler> _logger;

        public GetSeasonByIdQueryHandler(ISQLRepository<CultivationContext, CultivationSeason> seasons,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<GetSeasonByIdQueryHandler> logger)
        {
            _seasons = seasons;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<SeasonDetailResponse> Handle(GetSeasonByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _seasons.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<SeasonDetailResponse>(item);

        }
    }
}
