using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Identity.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Queries
{
    public class GetCertificatesQuery : IRequest<PagedList<CertificateDetailResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
        public Guid UserId { get; set; }
    }

    public class GetCertificatesQueryHandler : IRequestHandler<GetCertificatesQuery, PagedList<CertificateDetailResponse>>
    {

        private ISQLRepository<IdentityContext, Certificate> _certificates;
        private IUnitOfWork<IdentityContext> _unit;
        private ILogger<GetCertificatesQueryHandler> _logger;
        private IMapper _mapper;

        public GetCertificatesQueryHandler(ISQLRepository<IdentityContext, Certificate> certificates,
            IUnitOfWork<IdentityContext> unit,
            ILogger<GetCertificatesQueryHandler> logger,
            IMapper mapper)
        {
            _certificates = certificates;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedList<CertificateDetailResponse>> Handle(GetCertificatesQuery request, CancellationToken cancellationToken)
        {
            var rs = await _certificates.GetMany(e => e.MemberId == request.UserId
                                                && !e.Member.IsDeleted,
                                                ls => ls.Include(x=>x.Member)
                                                );
            
            return PagedList<CertificateDetailResponse>
                .ToPagedList(
                _mapper.Map<List<CertificateDetailResponse>>(rs),
                request.Pagination!.PageNumber,
                request.Pagination.PageSize);
        }
    }
}
