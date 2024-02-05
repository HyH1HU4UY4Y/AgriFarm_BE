using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedDomain.Entities.Users;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Certificates
{
    public class UpdateCertificateCommand: IRequest<CertificateDetailResponse>
    {
        public Guid Id { get; set; }
        public CertificateRequest Certificate { get; set; }
    }

    public class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, CertificateDetailResponse>
    {
        private ISQLRepository<IdentityContext, Certificate> _certs;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<UpdateCertificateCommandHandler> _logger;

        public UpdateCertificateCommandHandler(
            IMapper mapper, ISQLRepository<IdentityContext, Certificate> certs,
            IUnitOfWork<IdentityContext> unitOfWork,
            ILogger<UpdateCertificateCommandHandler> logger)
        {

            _mapper = mapper;
            _certs = certs;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CertificateDetailResponse> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {


            var cer = await _certs.GetOne(x => x.Id == request.Id);

            _mapper.Map(request.Certificate, cer);
            await _certs.UpdateAsync(cer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CertificateDetailResponse>(cer);
        }
    }
}
