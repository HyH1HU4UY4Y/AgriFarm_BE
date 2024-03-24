using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmScheduling.Commands.Activities;
using Service.FarmScheduling.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Defaults;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Queries.Activities
{
    public class GetActivitiesQuery: IRequest<PagedList<ActivityResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId { get; set; }
        public Guid SeasonId { get; set; }
        
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

        public async Task<PagedList<ActivityResponse>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var items = await _activities.GetMany(e=>e.SiteId == request.SiteId
                                                    && e.SeasonId == request.SeasonId,
                                                    ls=>ls.Include(x=>x.Participants)
                                                            .ThenInclude(x=>x.Participant)
                                                        .Include(x=>x.Season)
                                                        .Include(x=>x.Location)
                                                        .Include(x=>x.Addtions)
                                                    );
            
            var result = items.OrderByDescending(e => e.StartIn).ToList()
                        .Select(e =>
                        {
                            var x = _mapper.Map<ActivityResponse>(e);
                            x.Addition = _mapper.Map<AdditionResponse>(e.Addtions.FirstOrDefault());
                            x.Workers = _mapper.Map<List<UserResponse>>(e.Participants.Where(p =>
                                        p.Role == ActivityRole.Assignee.ToString())
                                        .Select(p => p.Participant).ToList());
                            x.Inspectors = _mapper.Map<List<UserResponse>>(e.Participants.Where(p =>
                                        p.Role == ActivityRole.Inspector.ToString())
                                        .Select(p => p.Participant).ToList());
                            return x;

                        });

            

            return PagedList<ActivityResponse>.ToPagedList(
                result,
                request.Pagination.PageNumber,
                request.Pagination.PageSize
            );
        }
    }
}
