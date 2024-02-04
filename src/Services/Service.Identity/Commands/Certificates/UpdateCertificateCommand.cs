using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using SharedDomain.Entities.Users;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Certificates
{
    public class UpdateCertificateCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public string Resource { get; set; }
    }

    public class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, Guid>
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

        public async Task<Guid> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {


            var cer = await _certs.GetOne(x => x.Id == request.Id);

            _mapper.Map(request, cer);
            await _certs.UpdateAsync(cer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cer.Id;
        }
    }
}
