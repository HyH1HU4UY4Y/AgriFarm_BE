using AutoMapper;
using Infrastructure.FarmScheduling.Contexts;
using MediatR;
using Service.FarmScheduling.DTOs;
using Service.FarmScheduling.Queries.Tags;
using SharedApplication.Pagination;
using SharedDomain.Entities.Schedules;
using SharedDomain.Repositories.Base;

namespace Service.FarmScheduling.Queries.Tags
{
    public class GetTagsQuery: IRequest<PagedList<TagResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, PagedList<TagResponse>>
    {
        private ISQLRepository<ScheduleContext, Tag> _tags;
        private IMapper _mapper;
        private IUnitOfWork<ScheduleContext> _unit;
        private ILogger<GetTagsQueryHandler> _logger;

        public GetTagsQueryHandler(ISQLRepository<ScheduleContext, Tag> tags,
            IMapper mapper,
            IUnitOfWork<ScheduleContext> unit,
            ILogger<GetTagsQueryHandler> logger)
        {
            _tags = tags;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
        }

        public async Task<PagedList<TagResponse>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var items = await _tags.GetMany();


            return PagedList<TagResponse>.ToPagedList(
                    _mapper.Map<List<TagResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
