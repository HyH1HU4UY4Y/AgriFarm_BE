using AutoMapper;
using Infrastructure.Soil.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Soil.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Queries
{
    public class GetLandWithPropertiesQuery: IRequest<LandWithPropertiesResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetLandWithPropertiesQueryHandler : IRequestHandler<GetLandWithPropertiesQuery, LandWithPropertiesResponse>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private ILogger<GetLandWithPropertiesQueryHandler> _logger;
        private IMapper _mapper;

        public GetLandWithPropertiesQueryHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            ILogger<GetLandWithPropertiesQueryHandler> logger,
            IMapper mapper)
        {
            _lands = lands;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LandWithPropertiesResponse> Handle(GetLandWithPropertiesQuery request, CancellationToken cancellationToken)
        {
            var item = await _lands.GetOne(e=>e.Id == request.Id, 
                                            ls => ls.Include(e => e.Properties)
                                                    .Include(e=>e.Site)        
                                            );

            if(item == null)
            {
                throw new NotFoundException("Land not exist.");
            }

            return _mapper.Map<LandWithPropertiesResponse>(item);
        }
    }
}
