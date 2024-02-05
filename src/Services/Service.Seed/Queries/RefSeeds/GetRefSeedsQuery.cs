using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.DTOs;
using Service.Seed.Queries.RefSeeds;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Queries.RefSeeds
{
    public class GetRefSeedsQuery : IRequest<PagedList<RefSeedResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetRefSeedsQueryHandler : IRequestHandler<GetRefSeedsQuery, PagedList<RefSeedResponse>>
    {

        private ISQLRepository<SeedlingContext, ReferencedSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefSeedsQueryHandler> _logger;

        public GetRefSeedsQueryHandler(ISQLRepository<SeedlingContext, ReferencedSeed> seeds,
            IMapper mapper,
            ILogger<GetRefSeedsQueryHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<RefSeedResponse>> Handle(GetRefSeedsQuery request, CancellationToken cancellationToken)
        {
            var items = await _seeds.GetMany();


            return PagedList<RefSeedResponse>.ToPagedList(
                    _mapper.Map<List<RefSeedResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
