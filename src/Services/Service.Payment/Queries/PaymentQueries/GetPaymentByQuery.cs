using AutoMapper;
using Infrastructure.Payment.Context;
using MediatR;
using Service.Payment.DTOs;
using SharedDomain.Entities.Pay;
using SharedDomain.Repositories.Base;

namespace Service.Payment.Queries.Payment
{
    public class GetPaymentByQuery : IRequest<PaymentDTO>
    {
        public Guid PaymentId { get; set; }
    }

    public class GetMerchantByQueryHandler : IRequestHandler<GetPaymentByQuery, PaymentDTO>
    {

        private ISQLRepository<PaymentContext, Merchant> _repo;
        private readonly IMapper _mapper;

        public GetMerchantByQueryHandler(IMapper mapper, ISQLRepository<PaymentContext, Merchant> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<PaymentDTO> Handle(GetPaymentByQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => e.Id == request.PaymentId);

            return _mapper.Map<PaymentDTO>(rs);

        }
    }
}
