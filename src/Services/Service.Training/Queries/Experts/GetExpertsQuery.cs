using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Training.Commands.Experts;
using Service.Training.DTOs;
using SharedApplication.Authorize.Values;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Training;
using SharedDomain.Repositories.Base;
using System.Security.Principal;

namespace Service.Training.Queries.Experts
{
    public class GetExpertsQuery : IRequest<PagedList<FullExpertResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid SiteId {  get; set; }
    }

    public class GetExpertsQueryHandler : IRequestHandler<GetExpertsQuery, PagedList<FullExpertResponse>>
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

        public async Task<PagedList<FullExpertResponse>> Handle(GetExpertsQuery request, CancellationToken cancellationToken)
        {
            var items = await _experts.GetMany(e=>e.SiteId == request.SiteId);



            return PagedList<FullExpertResponse>.ToPagedList(
                    _mapper.Map<List<FullExpertResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
