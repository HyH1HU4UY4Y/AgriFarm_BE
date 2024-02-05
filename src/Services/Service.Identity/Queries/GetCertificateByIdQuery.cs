using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Queries
{
    public class GetCertificateByIdQuery: IRequest<CertificateDetailResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler : IRequestHandler<GetCertificateByIdQuery, CertificateDetailResponse>
    {

        private ISQLRepository<IdentityContext, Certificate> _certificates;
        private IUnitOfWork<IdentityContext> _unit;
        private ILogger<GetSupplierByIdQueryHandler> _logger;
        private IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISQLRepository<IdentityContext, Certificate> certificates,
            IUnitOfWork<IdentityContext> unit,
            ILogger<GetSupplierByIdQueryHandler> logger,
            IMapper mapper)
        {
            _certificates = certificates;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CertificateDetailResponse> Handle(GetCertificateByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _certificates.GetOne(e => e.Id == request.Id);
            if (rs == null)
            {
                throw new NotFoundException("item not exist");
            }

            return _mapper.Map<CertificateDetailResponse>(rs);
        }
    }
}
