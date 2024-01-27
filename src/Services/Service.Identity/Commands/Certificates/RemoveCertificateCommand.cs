using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Entities.Users;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Certificates
{
    public class RemoveCertificateCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }

    }

    public class RemoveCertificateCommandHandler : IRequestHandler<RemoveCertificateCommand, Guid>
    {
        private ISQLRepository<IdentityContext, Certificate> _certs;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<RemoveCertificateCommandHandler> _logger;

        public RemoveCertificateCommandHandler(
            IMapper mapper, ISQLRepository<IdentityContext, Certificate> certs,
            IUnitOfWork<IdentityContext> unitOfWork,
            ILogger<RemoveCertificateCommandHandler> logger)
        {

            _mapper = mapper;
            _certs = certs;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(RemoveCertificateCommand request, CancellationToken cancellationToken)
        {
            var cer = await _certs.GetOne(x=> x.Id == request.Id);
            
            if(cer == null)
            {
                throw new NotFoundException();
            }

            await _certs.SoftDeleteAsync(cer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cer.Id;
        }
    }
}
