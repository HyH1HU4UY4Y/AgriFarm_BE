using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Seasons
{
    public class GetSeasonsQuery : IRequest<PagedList<SeasonDetailResponse>>
    {
        public PaginationRequest Pagination { get; set; }
    }

    public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, PagedList<SeasonDetailResponse>>
    {
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<GetSeasonsQueryHandler> _logger;

        public GetSeasonsQueryHandler(ISQLRepository<CultivationContext, CultivationSeason> seasons,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<GetSeasonsQueryHandler> logger)
        {
            _seasons = seasons;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<PagedList<SeasonDetailResponse>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
        {
            var items = await _seasons.GetMany();


            return PagedList<SeasonDetailResponse>.ToPagedList(
                    _mapper.Map<List<SeasonDetailResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
