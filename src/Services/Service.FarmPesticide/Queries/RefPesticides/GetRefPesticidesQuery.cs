using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Service.Pesticide.Commands.RefPesticides;
using Service.Pesticide.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Queries.RefPesticides
{
    public class GetRefPesticidesQuery : IRequest<PagedList<RefPesticideResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetRefPesticidesQueryHandler : IRequestHandler<GetRefPesticidesQuery, PagedList<RefPesticideResponse>> 
    { 
        private ISQLRepository<FarmPesticideContext, ReferencedPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefPesticidesQueryHandler> _logger;

        public GetRefPesticidesQueryHandler(ISQLRepository<FarmPesticideContext, ReferencedPesticide> pesticides,
            IMapper mapper,
            ILogger<GetRefPesticidesQueryHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<RefPesticideResponse>> Handle(GetRefPesticidesQuery request, CancellationToken cancellationToken)
        {
            var items = await _pesticides.GetMany();


            return PagedList<RefPesticideResponse>.ToPagedList(
                    _mapper.Map<List<RefPesticideResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
