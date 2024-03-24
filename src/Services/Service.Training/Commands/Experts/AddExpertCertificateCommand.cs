using AutoMapper;
using Infrastructure.Training.Contexts;
using MediatR;
using Service.Training.DTOs;
using SharedDomain.Entities.Training;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Training.Commands.Experts
{
    public class AddExpertCertificateCommand: IRequest<FullExpertResponse>
    {
        public Guid ExpertId { get; set; }
        public Guid SiteId { get; set; }

        public ExpertCertification Certificate { get; set; }
    }

    public class AddExpertCertificateCommandHandler : IRequestHandler<AddExpertCertificateCommand, FullExpertResponse>
    {

        private ISQLRepository<TrainingContext, ExpertInfo> _experts;
        private IUnitOfWork<TrainingContext> _unit;
        private IMapper _mapper;
        private ILogger<AddExpertCertificateCommandHandler> _logger;

        public AddExpertCertificateCommandHandler(ISQLRepository<TrainingContext, ExpertInfo> experts,
            IMapper mapper,
            ILogger<AddExpertCertificateCommandHandler> logger,
            IUnitOfWork<TrainingContext> unit)
        {
            _experts = experts;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<FullExpertResponse> Handle(AddExpertCertificateCommand request, CancellationToken cancellationToken)
        {
            var expert = await _experts.GetOne(e=>e.Id == request.ExpertId && e.SiteId == request.SiteId);
            if (expert == null) throw new NotFoundException();
            if (expert.Certificates == null) expert.Certificates = new();
            if(expert.Certificates.Any(e=>e.Name == request.Certificate.Name)) 
                throw new BadRequestException("This certificate has already exist.");

            expert.Certificates.Add(_mapper.Map<ExpertCertification>(request.Certificate));
            await _experts.UpdateAsync(expert);
            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FullExpertResponse>(expert);
        }
    }
}
