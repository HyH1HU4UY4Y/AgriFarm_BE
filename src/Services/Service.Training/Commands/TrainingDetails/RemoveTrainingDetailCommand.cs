using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.Queries.TrainingDetails;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TrainingDetails
{
    public class RemoveTrainingDetailCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveTrainingDetailCommandHandler : IRequestHandler<RemoveTrainingDetailCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveTrainingDetailCommandHandler> _logger;

        public RemoveTrainingDetailCommandHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<RemoveTrainingDetailCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveTrainingDetailCommand request, CancellationToken cancellationToken)
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
