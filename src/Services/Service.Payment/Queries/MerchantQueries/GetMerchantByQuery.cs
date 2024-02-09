using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs.MerchantDTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Pay;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Queries.MerchantQueries
{
    public class GetMerchantByQuery : IRequest<MerchantDTO>
    {
        public Guid MerchantId { get; set; }
    }

    public class GetMerchantByQueryHandler : IRequestHandler<GetMerchantByQuery, MerchantDTO>
    {

        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IMapper _mapper;

        public GetMerchantByQueryHandler(IMapper mapper, ISQLRepository<PaymentContext, Merchant> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<MerchantDTO> Handle(GetMerchantByQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => e.Id == request.MerchantId);

            return _mapper.Map<MerchantDTO>(rs);

        }
    }
}
