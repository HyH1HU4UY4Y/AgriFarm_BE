using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Seasons
{
    public class UpdateSeasonCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public SeasonRequest Season { get; set; }
    }

    public class UpdateSeasonCommandHandler : IRequestHandler<UpdateSeasonCommand, Guid>
    {
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<CreateSeasonCommandHandler> _logger;

        public UpdateSeasonCommandHandler(ISQLRepository<CultivationContext, CultivationSeason> seasons,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<CreateSeasonCommandHandler> logger)
        {
            _seasons = seasons;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<Guid> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            var item = await _seasons.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Season, item);

            await _seasons.UpdateAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;
        }
    }
}
