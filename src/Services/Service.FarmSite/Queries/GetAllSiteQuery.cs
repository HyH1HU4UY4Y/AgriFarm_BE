using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Service.FarmSite.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Queries
{
    public class GetAllSiteQuery: IRequest<PagedList<SiteResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetAllSiteQueryHandler : IRequestHandler<GetAllSiteQuery, PagedList<SiteResponse>>
    {
        private readonly ISQLRepository<SiteContext, Site> _repo;
        private readonly IMapper _mapper;

        public GetAllSiteQueryHandler(ISQLRepository<SiteContext, Site> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<PagedList<SiteResponse>> Handle(GetAllSiteQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.GetMany().Result!
                        .OrderBy (x => x.Name)
                        .ToList ();



            return Task.FromResult(
                PagedList<SiteResponse>.ToPagedList(
                    _mapper.Map<List<SiteResponse>>(rs),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                ));
        }
    }
}
