using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmScheduling.DTOs.Details;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Queries.Activities
{
    public class GetTreatmentDetailQuery: IRequest<TreatmentDetailResponse>
    {
        public Guid ActivityId { get; set; }


    }

    public class GetTreatmentDetailQueryHandler : IRequestHandler<GetTreatmentDetailQuery, TreatmentDetailResponse>
    {
        private ISQLRepository<ScheduleContext, TreatmentDetail> _details;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<GetTreatmentDetailQueryHandler> _logger;

        public GetTreatmentDetailQueryHandler(
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<GetTreatmentDetailQueryHandler> logger,
            ISQLRepository<ScheduleContext, TreatmentDetail> details)
        {
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _details = details;
        }

        public async Task<TreatmentDetailResponse> Handle(GetTreatmentDetailQuery request, CancellationToken cancellationToken)
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


            return _mapper.Map<TreatmentDetailResponse>(item);
        }
    }
}
