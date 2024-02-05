﻿using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Entities.Users;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Commands.Certificates
{
    public class AddCertificateCommand: IRequest<CertificateResponse>
    {
        public Guid UserId { get; set; }
        public CertificateRequest Certificate { get; set; }
    }

    public class AddCertificateCommandHandler : IRequestHandler<AddCertificateCommand, CertificateResponse>
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

        public async Task<CertificateResponse> Handle(AddCertificateCommand request, CancellationToken cancellationToken)
        {
            var cer = _mapper.Map<Certificate>(request.Certificate);
            var rs = await _certs.AddAsync(cer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CertificateResponse>(rs);
        }
    }

}
