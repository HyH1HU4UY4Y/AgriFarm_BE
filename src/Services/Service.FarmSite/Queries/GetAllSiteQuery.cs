using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Service.FarmSite.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Queries
{
    public class GetAllSiteQuery: IRequest<List<SiteResponse>>
    {
        
    }

    public class GetAllSiteQueryHandler : IRequestHandler<GetAllSiteQuery, List<SiteResponse>>
    {
        private readonly ISQLRepository<SiteContext, Site> _repo;
        private readonly IMapper _mapper;

        public GetAllSiteQueryHandler(ISQLRepository<SiteContext, Site> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<List<SiteResponse>> Handle(GetAllSiteQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.GetMany().Result!
                        .OrderBy (x => x.Name)
                        .ToList ();

            return Task.FromResult(_mapper.Map<List<SiteResponse>>(rs));
        }
    }
}
