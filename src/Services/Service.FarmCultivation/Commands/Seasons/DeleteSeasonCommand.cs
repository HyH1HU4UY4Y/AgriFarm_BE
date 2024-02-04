using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Queries.Seasons
{
    public class DeleteSeasonCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, Guid>
    {
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<DeleteSeasonCommandHandler> _logger;

        public DeleteSeasonCommandHandler(ISQLRepository<CultivationContext, CultivationSeason> seasons,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<DeleteSeasonCommandHandler> logger)
        {
            _seasons = seasons;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<Guid> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var item = await _seasons.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _seasons.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;

        }
    }
}
