using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.Commands.Activities;
using Service.FarmScheduling.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Queries.Activities
{
    public class GetActivitiesQuery: IRequest<PagedList<ActivityResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        
    }

    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, PagedList<ActivityResponse>>
    {
        private ISQLRepository<ScheduleContext, Activity> _activities;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<GetActivitiesQueryHandler> _logger;

        public GetActivitiesQueryHandler(ISQLRepository<ScheduleContext, Activity> activities,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<GetActivitiesQueryHandler> logger)
        {
            _activities = activities;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public Task<PagedList<ActivityResponse>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
