using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmScheduling.DTOs.Details;
using SharedDomain.Defaults.Converters;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Queries.Activities
{
    public class GetUsingDetailQuery : IRequest<UsingDetailResponse>
    {
        public Guid ActivityId { get; set; }
        public Guid UserId { get; set; }
    }


    public class GetUsingDetailQueryHandler : IRequestHandler<GetUsingDetailQuery, UsingDetailResponse>
    {
        private ISQLRepository<ScheduleContext, UsingDetail> _details;
        private ISQLRepository<ScheduleContext, BaseComponent> _components;
        private ScheduleContext _context;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<GetUsingDetailQueryHandler> _logger;

        public GetUsingDetailQueryHandler(
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<GetUsingDetailQueryHandler> logger,
            ISQLRepository<ScheduleContext, UsingDetail> details,
            ISQLRepository<ScheduleContext, BaseComponent> components,
            ScheduleContext context)
        {
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _details = details;
            _components = components;
            _context = context;
        }

        public async Task<UsingDetailResponse> Handle(GetUsingDetailQuery request, CancellationToken cancellationToken)
        {
            var item = await _details.GetOne(e => e.ActivityId == request.ActivityId
                                                && !e.Activity.IsDeleted,
                                                   ls => ls.Include(x => x.Activity)
                                                        .Include(x => x.Component)
                                                    );

            if (item == null)
            {
                throw new NotFoundException();
            }


            return _mapper.Map<UsingDetailResponse>(item);
        }
    }
}
