using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Service.FarmSite.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Queries
{
    public class GetFarmByIdQuery: IRequest<FullSiteResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdFarmQueryHandler : IRequestHandler<GetFarmByIdQuery, FullSiteResponse>
    {
        private readonly ISQLRepository<SiteContext, Site> _repo;
        private readonly IMapper _mapper;

        public GetByIdFarmQueryHandler(ISQLRepository<SiteContext, Site> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<FullSiteResponse> Handle(GetFarmByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _repo.GetOne(e => e.Id == request.Id);

            if( item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<FullSiteResponse>(item);
        }
    }
}
