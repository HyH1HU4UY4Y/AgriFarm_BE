using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;


namespace Service.FarmCultivation.Queries.Seasons
{
    public class CreateSeasonCommand : IRequest<Guid>
    {
        public SeasonRequest Season { get; set; }
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, Guid>
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

        public Task<Guid> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
