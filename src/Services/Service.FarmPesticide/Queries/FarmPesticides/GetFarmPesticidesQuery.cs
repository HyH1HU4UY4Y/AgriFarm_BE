using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Pesticide.Commands.FarmPesticides;
using Service.Pesticide.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Queries.FarmPesticides
{
    public class GetFarmPesticidesQuery : IRequest<PagedList<PesticideResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetFarmPesticidesQueryHandler : IRequestHandler<GetFarmPesticidesQuery, PagedList<PesticideResponse>>
    {

        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmPesticidesQueryHandler> _logger;

        public GetFarmPesticidesQueryHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<GetFarmPesticidesQueryHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PagedList<PesticideResponse>> Handle(GetFarmPesticidesQuery request, CancellationToken cancellationToken)
        {
            var items = await _pesticides.GetMany(null, ls => ls.Include(x => x.Properties));



            return PagedList<PesticideResponse>.ToPagedList(
                    _mapper.Map<List<PesticideResponse>>(items),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                );
        }
    }
}
