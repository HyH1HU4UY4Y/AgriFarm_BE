using AutoMapper;
using Infrastructure.Soil.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Soil.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Queries
{
    public class GetLandsWithPropertiesQuery: IRequest<PagedList<LandWithPropertiesResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        //public Guid Id { get; set; }

    }

    public class GetLandsWithPropertiesQueryHandler : IRequestHandler<GetLandsWithPropertiesQuery, PagedList<LandWithPropertiesResponse>>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private ILogger<GetLandsWithPropertiesQueryHandler> _logger;
        private IMapper _mapper;

        public GetLandsWithPropertiesQueryHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            ILogger<GetLandsWithPropertiesQueryHandler> logger,
            IMapper mapper)
        {
            _lands = lands;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedList<LandWithPropertiesResponse>> Handle(GetLandsWithPropertiesQuery request, CancellationToken cancellationToken)
        {
            var items = await _lands.GetMany(null, 
                        ls => ls.Include(e=>e.Properties)
                                .Include(e=>e.Site)
                        );


            return PagedList<LandWithPropertiesResponse>
                    .ToPagedList(_mapper.Map<List<LandWithPropertiesResponse>>(items), 
                        request.Pagination.PageNumber,
                        request.Pagination.PageSize);
        }
    }
}
