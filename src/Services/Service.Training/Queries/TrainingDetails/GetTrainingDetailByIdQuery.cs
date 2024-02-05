using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingDetails
{
    public class GetTrainingDetailByIdQuery : IRequest<TrainingDetailResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetTrainingDetailByIdQueryHandler : IRequestHandler<GetTrainingDetailByIdQuery, TrainingDetailResponse>
    {

        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetTrainingDetailByIdQueryHandler> _logger;

        public GetTrainingDetailByIdQueryHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<GetTrainingDetailByIdQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<TrainingDetailResponse> Handle(GetTrainingDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _trainings.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<TrainingDetailResponse>(item);
        }
    }
}
