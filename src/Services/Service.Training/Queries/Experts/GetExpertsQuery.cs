using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Training.Commands.Experts;
using Service.Training.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Training.Queries.Experts
{
    public class GetExpertsQuery : IRequest<PagedList<ExpertResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetExpertsQueryHandler : IRequestHandler<GetExpertsQuery, PagedList<ExpertResponse>>
    {

        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetExpertsQueryHandler> _logger;

        public GetExpertsQueryHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<GetExpertsQueryHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<ExpertResponse>> Handle(GetExpertsQuery request, CancellationToken cancellationToken)
        {
            var items = await _experts.GetMany();



            return PagedList<ExpertResponse>.ToPagedList(
                    _mapper.Map<List<ExpertResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
