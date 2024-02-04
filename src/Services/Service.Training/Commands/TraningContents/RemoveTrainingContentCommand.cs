using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.Queries.TrainingContents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TraningContents
{
    public class RemoveTrainingContentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveTrainingContentCommandHandler : IRequestHandler<RemoveTrainingContentCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingContent> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveTrainingContentCommandHandler> _logger;

        public RemoveTrainingContentCommandHandler(ISQLRepository<TrainingContext, TrainingContent> trainings,
            IMapper mapper,
            ILogger<RemoveTrainingContentCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveTrainingContentCommand request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _trainings.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _trainings.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
