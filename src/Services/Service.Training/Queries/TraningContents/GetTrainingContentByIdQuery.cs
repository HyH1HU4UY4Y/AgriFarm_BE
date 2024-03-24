using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.TrainingContents
{
    public class GetTrainingContentByIdQuery : IRequest<FullContentResponse>
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
    }

    public class GetTrainingContentByIdQueryHandler : IRequestHandler<GetTrainingContentByIdQuery, FullContentResponse>
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

        public async Task<FullContentResponse> Handle(GetTrainingContentByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _trainings.GetOne(e => e.Id == request.Id && e.SiteId == request.SiteId);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<FullContentResponse>(item);
        }
    }
}
