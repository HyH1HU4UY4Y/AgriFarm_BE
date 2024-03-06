using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs;
using Service.FarmCultivation.DTOs.Seasons;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;


namespace Service.FarmCultivation.Queries.Seasons
{
    public class CreateSeasonCommand : IRequest<SeasonDetailResponse>
    {
        public SeasonCreateRequest Season { get; set; }
        public Guid SiteId { get; set; }
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, SeasonDetailResponse>
    {
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<CreateSeasonCommandHandler> _logger;

        public CreateSeasonCommandHandler(ISQLRepository<CultivationContext, CultivationSeason> seasons,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<CreateSeasonCommandHandler> logger)
        {
            _seasons = seasons;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<SeasonDetailResponse> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {

            var item = _mapper.Map<CultivationSeason>(request.Season);
            item.SiteId = request.SiteId;

            if(item.Products.Any())
            {
                foreach (var p in item.Products)
                {
                    p.SiteId = request.SiteId;
                    p.Name = $"{p.Seed.Name} ({p.Land.Name})";
                    p.SeedId = p.Seed.Id;
                    p.LandId = p.Land.Id;
                    p.Seed = null;
                    p.Land = null;
                }
            }
            
            await _seasons.AddAsync(item);
            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SeasonDetailResponse>(item);
        }
    }
}
