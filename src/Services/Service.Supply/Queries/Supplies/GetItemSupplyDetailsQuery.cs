using AutoMapper;
using Infrastructure.Supply.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Supply.DTOs;
using Service.Supply.Queries.Suppliers;
using SharedApplication.Pagination;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Repositories.Base;

namespace Service.Supply.Queries.Supplies
{
    public class GetItemSupplyDetailsQuery : IRequest<PagedList<SupplyDetailResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid ItemId { get; set; }
    }

    public class GetItemSupplyDetailsQueryHandler : IRequestHandler<GetItemSupplyDetailsQuery, PagedList<SupplyDetailResponse>>
    {
        private ISQLRepository<SupplyContext, SupplyDetail> _supplies;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<GetItemSupplyDetailsQueryHandler> _logger;
        private IMapper _mapper;

        public GetItemSupplyDetailsQueryHandler(ISQLRepository<SupplyContext, SupplyDetail> supplies,
            IUnitOfWork<SupplyContext> unit,
            ILogger<GetItemSupplyDetailsQueryHandler> logger,
            IMapper mapper)
        {
            _supplies = supplies;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedList<SupplyDetailResponse>> Handle(GetItemSupplyDetailsQuery request, CancellationToken cancellationToken)
        {
            var items = await _supplies.GetMany(e=>e.ComponentId == request.ItemId,
                                                ls=>ls.Include(x=>x.Supplier));

            return PagedList<SupplyDetailResponse>.ToPagedList(
                        _mapper.Map<List<SupplyDetailResponse>>(items),
                        request.Pagination.PageNumber,
                        request.Pagination.PageSize
                    );
        }
    }
}
