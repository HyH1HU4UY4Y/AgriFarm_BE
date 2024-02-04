using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Fertilize.Commands.FarmFertilizes;
using Service.Fertilize.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Queries.FarmFertilizes
{
    public class GetFarmFertilizesQuery : IRequest<PagedList<FertilizeResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetFarmFertilizesQueryHandler : IRequestHandler<GetFarmFertilizesQuery, PagedList<FertilizeResponse>>
    {

        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmFertilizesQueryHandler> _logger;

        public GetFarmFertilizesQueryHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<GetFarmFertilizesQueryHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<FertilizeResponse>> Handle(GetFarmFertilizesQuery request, CancellationToken cancellationToken)
        {
            var items = await _fertilizes.GetMany(null, ls => ls.Include(x => x.Properties));



            return PagedList<FertilizeResponse>.ToPagedList(
                    _mapper.Map<List<FertilizeResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
