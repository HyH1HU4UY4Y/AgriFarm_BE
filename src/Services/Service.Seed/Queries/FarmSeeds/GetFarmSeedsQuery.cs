﻿using AutoMapper;
using Infrastructure.Seed.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Seed.Commands;
using Service.Seed.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Seed.Queries.FarmSeeds
{
    public class GetFarmSeedsQuery : IRequest<PagedList<SeedResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetFarmSeedsQueryHandler : IRequestHandler<GetFarmSeedsQuery, PagedList<SeedResponse>>
    {

        private ISQLRepository<SeedlingContext, FarmSeed> _seeds;
        private IUnitOfWork<SeedlingContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmSeedsQueryHandler> _logger;

        public GetFarmSeedsQueryHandler(ISQLRepository<SeedlingContext, FarmSeed> seeds,
            IMapper mapper,
            ILogger<GetFarmSeedsQueryHandler> logger,
            IUnitOfWork<SeedlingContext> unit)
        {
            _seeds = seeds;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<SeedResponse>> Handle(GetFarmSeedsQuery request, CancellationToken cancellationToken)
        {
            var items = await _seeds.GetMany(null, ls => ls.Include(x => x.Properties));

            

            return PagedList<SeedResponse>.ToPagedList(
                    _mapper.Map<List<SeedResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}