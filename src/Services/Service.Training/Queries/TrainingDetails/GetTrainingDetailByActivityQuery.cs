using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Training.DTOs;
using SharedDomain.Entities.Schedules.Additions;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingDetails
{
    public class GetTrainingDetailByActivityQuery : IRequest<DetailResponse>
    {
        public Guid ActivityId { get; set; }
    }

    public class GetTrainingDetailByActivityQueryHandler : IRequestHandler<GetTrainingDetailByActivityQuery, DetailResponse>
    {

        private ISQLRepository<TrainingContext, TrainingDetail> _trainings;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetTrainingDetailByActivityQueryHandler> _logger;

        public GetTrainingDetailByActivityQueryHandler(ISQLRepository<TrainingContext, TrainingDetail> trainings,
            IMapper mapper,
            ILogger<GetTrainingDetailByActivityQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _trainings = trainings;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<DetailResponse> Handle(GetTrainingDetailByActivityQuery request, CancellationToken cancellationToken)
        {
            var item = await _trainings.GetOne(e => e.ActivityId == request.ActivityId,
                                               ls=>ls.Include(x=>x.Content)
                                                    .Include(x=>x.Expert)
                                               );

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<DetailResponse>(item);
        }
    }
}
