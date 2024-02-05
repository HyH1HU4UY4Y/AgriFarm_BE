using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingContents
{
    public class GetTrainingContentsQuery : IRequest<PagedList<TrainingContentResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetTrainingContentsQueryHandler : IRequestHandler<GetTrainingContentsQuery, PagedList<TrainingContentResponse>>
    {

        private ISQLRepository<TrainingContext, TrainingContent> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetTrainingContentsQueryHandler> _logger;

        public GetTrainingContentsQueryHandler(ISQLRepository<TrainingContext, TrainingContent> trainings,
            IMapper mapper,
            ILogger<GetTrainingContentsQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<TrainingContentResponse>> Handle(GetTrainingContentsQuery request, CancellationToken cancellationToken)
        {
            var items = await _trainings.GetMany();



            return PagedList<TrainingContentResponse>.ToPagedList(
                    _mapper.Map<List<TrainingContentResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
