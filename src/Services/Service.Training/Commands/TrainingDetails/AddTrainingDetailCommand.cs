using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingDetails;
using SharedDomain.Defaults;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TrainingDetails
{
    public class AddTrainingDetailCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DetailRequest TrainingDetail { get; set; }
        public Guid ActivityId { get; set; }
        public Guid ContentId { get; set; }
        public Guid ExpertId { get; set; }
        public string AdditionType { get; set; }
        public string? Description { get; set; }
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

            

            await _trainings.AddAsync(new()
            {
                Id = request.Id,
                ActivityId = request.ActivityId,
                ContentId = request.ContentId,
                ExpertId = request.ExpertId,
                AdditionType = AdditionType.Training,
                Description = request.Description
            });

            await _unit.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
