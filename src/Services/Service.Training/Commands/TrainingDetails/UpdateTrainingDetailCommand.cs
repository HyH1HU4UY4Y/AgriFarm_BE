using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingDetails;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TrainingDetails
{
    public class UpdateTrainingDetailCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public TrainingDetailRequest TrainingDetail { get; set; }
    }

    public class UpdateTrainingDetailCommandHandler : IRequestHandler<UpdateTrainingDetailCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateTrainingDetailCommandHandler> _logger;

        public UpdateTrainingDetailCommandHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<UpdateTrainingDetailCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(UpdateTrainingDetailCommand request, CancellationToken cancellationToken)
        {
            var item = await _trainings.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.TrainingDetail, item);

            await _trainings.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
