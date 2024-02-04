using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using Service.Training.Queries.TrainingContents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.TraningContents
{
    public class UpdateTrainingContentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public TrainingContentRequest TrainingContent { get; set; }
    }

    public class UpdateTrainingContentCommandHandler : IRequestHandler<UpdateTrainingContentCommand, Guid>
    {
        private ISQLRepository<TrainingContext, TrainingContent> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateTrainingContentCommandHandler> _logger;

        public UpdateTrainingContentCommandHandler(ISQLRepository<TrainingContext, TrainingContent> trainings,
            IMapper mapper,
            ILogger<UpdateTrainingContentCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(UpdateTrainingContentCommand request, CancellationToken cancellationToken)
        {
            var item = await _trainings.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.TrainingContent, item);

            await _trainings.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
