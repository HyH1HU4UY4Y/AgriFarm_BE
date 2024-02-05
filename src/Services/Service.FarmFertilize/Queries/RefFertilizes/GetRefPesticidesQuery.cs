using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Service.Fertilize.Commands.RefFertilizes;
using Service.Fertilize.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Queries.RefFertilizes
{
    public class GetRefFertilizesQuery : IRequest<PagedList<RefFertilizeResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetRefFertilizesQueryHandler : IRequestHandler<GetRefFertilizesQuery, PagedList<RefFertilizeResponse>>
    {
        private ISQLRepository<FarmFertilizeContext, ReferencedFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefFertilizesQueryHandler> _logger;

        public GetRefFertilizesQueryHandler(ISQLRepository<FarmFertilizeContext, ReferencedFertilize> fertilizes,
            IMapper mapper,
            ILogger<GetRefFertilizesQueryHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<RefFertilizeResponse>> Handle(GetRefFertilizesQuery request, CancellationToken cancellationToken)
        {
            var items = await _fertilizes.GetMany();


            return PagedList<RefFertilizeResponse>.ToPagedList(
                    _mapper.Map<List<RefFertilizeResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
