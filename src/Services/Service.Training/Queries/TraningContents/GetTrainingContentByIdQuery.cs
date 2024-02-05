using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingContents
{
    public class GetTrainingContentByIdQuery : IRequest<TrainingContentResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetTrainingContentByIdQueryHandler : IRequestHandler<GetTrainingContentByIdQuery, TrainingContentResponse>
    {

        private ISQLRepository<TrainingContext, TrainingContent> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetTrainingContentByIdQueryHandler> _logger;

        public GetTrainingContentByIdQueryHandler(ISQLRepository<TrainingContext, TrainingContent> trainings,
            IMapper mapper,
            ILogger<GetTrainingContentByIdQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<TrainingContentResponse> Handle(GetTrainingContentByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _trainings.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<TrainingContentResponse>(item);
        }
    }
}
