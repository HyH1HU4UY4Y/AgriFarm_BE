﻿using AutoMapper;
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
    public class GetLandBySiteCodeQuery: IRequest<LandResponse>
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
    }

    public class GetLandBySiteCodeQueryHandler : IRequestHandler<GetLandBySiteCodeQuery, LandResponse>
    {
        private ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private IUnitOfWork<FarmSoilContext> _unit;
        private ILogger<GetLandBySiteCodeQueryHandler> _logger;
        private IMapper _mapper;

        public GetLandBySiteCodeQueryHandler(ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IUnitOfWork<FarmSoilContext> unit,
            ILogger<GetLandBySiteCodeQueryHandler> logger,
            IMapper mapper)
        {
            _lands = lands;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<LandResponse> Handle(GetLandBySiteCodeQuery request, CancellationToken cancellationToken)
        {
            var items = await _lands.GetOne(
                e => e.SiteId == request.SiteId
                //, ls => ls.Include(x => x.Site)
                );

            if(items == null)
            {
                throw new NotFoundException("Land not exist");
            }


            return _mapper.Map<LandResponse>(items);
        }
    }
}
