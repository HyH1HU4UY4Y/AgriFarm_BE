using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingDetails;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TrainingDetails
{
    public class AddTrainingDetailCommand : IRequest<Guid>
    {
        public TrainingDetailRequest TrainingDetail { get; set; }
    }

    public class AddTrainingDetailCommandHandler : IRequestHandler<AddTrainingDetailCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddTrainingDetailCommandHandler> _logger;

        public AddTrainingDetailCommandHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<AddTrainingDetailCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddTrainingDetailCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with TrainingDetail info
            */

            var item = _mapper.Map<TrainingDetail>(request.TrainingDetail);

            await _trainings.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
