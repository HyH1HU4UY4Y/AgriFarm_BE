using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Entities.Users;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Certificates
{
    public class AddCertificateCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public string Resource { get; set; }
    }

    public class AddCertificateCommandHandler : IRequestHandler<AddCertificateCommand, Guid>
    {
        private ISQLRepository<IdentityContext, Certificate> _certs;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<AddCertificateCommandHandler> _logger;

        public AddCertificateCommandHandler(
            IMapper mapper, ISQLRepository<IdentityContext, Certificate> certs,
            IUnitOfWork<IdentityContext> unitOfWork, 
            ILogger<AddCertificateCommandHandler> logger)
        {
            _mapper = mapper;
            _certs = certs;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddCertificateCommand request, CancellationToken cancellationToken)
        {
            var cer = _mapper.Map<Certificate>(request);
            var rs = await _certs.AddAsync(cer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return rs.Id;
        }
    }

}
