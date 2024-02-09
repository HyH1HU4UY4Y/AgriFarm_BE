using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Entities.Pay;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Queries.MerchantQueries
{
    public class GetMerchantQuery : IRequest<List<MerchantDTO>>
    {
        public string? keyword { get; set; }
        public string? searchDateFrom { get; set; }
        public string? searchDateTo { get; set; }
        public int perPage { get; set; }
        public int pageId { get; set; }
        public bool getAllDataFlag { get; set; } = true;
    }

    public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, List<MerchantDTO>>
    {

        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IMapper _mapper;

        public GetMerchantQueryHandler(IMapper mapper, ISQLRepository<PaymentContext, Merchant> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<List<MerchantDTO>> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetMany();

            if (rs != null)
            {
                // Search by keyword
                if (request.keyword != null)
                {
                    /*rs = rs.Where(k => (k.DiseaseName!.Contains(request.keyword) ||
                                k.Symptoms!.Contains(request.keyword) ||
                                k.Cause!.Contains(request.keyword))
                        ).ToList();*/
                }
                // Search by date
                DateTime dateFrom;
                DateTime dateTo;
                if (DateTime.TryParse(request.searchDateFrom, out dateFrom))
                {
                    rs = rs.Where(k => k.CreatedDate >= dateFrom).ToList();
                }
                if (DateTime.TryParse(request.searchDateTo, out dateTo))
                {
                    rs = rs.Where(k => k.CreatedDate <= dateTo).ToList();
                }
                // Pagination
                if (!request.getAllDataFlag)
                {
                    rs = rs.Skip(request.perPage * (request.pageId - 1)).Take(request.perPage).ToList();
                }
            }
            return _mapper.Map<List<MerchantDTO>>(rs);

        }
    }
}
