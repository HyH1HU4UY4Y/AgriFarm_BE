using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingContents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TraningContents
{
    public class AddTrainingContentCommand : IRequest<Guid>
    {
        public TrainingContentRequest TrainingContent { get; set; }
    }

    public class AddTrainingContentCommandHandler : IRequestHandler<AddTrainingContentCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingContent> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddTrainingContentCommandHandler> _logger;

        public AddTrainingContentCommandHandler(ISQLRepository<TrainingContext, TrainingContent> trainings,
            IMapper mapper,
            ILogger<AddTrainingContentCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddTrainingContentCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with TrainingContent info
            */

            var item = _mapper.Map<TrainingContent>(request.TrainingContent);

            await _trainings.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
