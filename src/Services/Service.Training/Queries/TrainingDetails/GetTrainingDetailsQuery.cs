using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingDetails
{
    public class GetTrainingDetailsQuery : IRequest<PagedList<TrainingDetailResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetTrainingDetailsQueryHandler : IRequestHandler<GetTrainingDetailsQuery, PagedList<TrainingDetailResponse>>
    {

        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetTrainingDetailsQueryHandler> _logger;

        public GetTrainingDetailsQueryHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<GetTrainingDetailsQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<TrainingDetailResponse>> Handle(GetTrainingDetailsQuery request, CancellationToken cancellationToken)
        {
            var items = await _trainings.GetMany();



            return PagedList<TrainingDetailResponse>.ToPagedList(
                    _mapper.Map<List<TrainingDetailResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
