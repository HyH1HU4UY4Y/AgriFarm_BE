using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using SharedDomain.Entities.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.Experts
{
    public class DeleteExpertCertificateCommand  : IRequest<Guid>
    {
        public Guid ExpertId { get; set; }
        public string CertificateName { get; set; }
    }

    public class DeleteExpertCertificateCommandHandler : IRequestHandler<DeleteExpertCertificateCommand , Guid>
    {
        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<DeleteExpertCertificateCommandHandler> _logger;

        public DeleteExpertCertificateCommandHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<DeleteExpertCertificateCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(DeleteExpertCertificateCommand  request, CancellationToken cancellationToken)
        {
            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _experts.GetOne(e => e.Id == request.ExpertId);

            if (item == null || item.Certificates ==  null)
            {
                throw new NotFoundException("Item not exist");
            }

            var cer = item.Certificates.FirstOrDefault(e => e.Name == request.CertificateName);
            item.Certificates.Remove(cer);

            await _experts.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
