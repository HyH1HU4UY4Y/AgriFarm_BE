using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Commands
{
    public class CreateDiseaseDiagnosesCommand : IRequest<DiseaseDiagnosesDTO>
    {
        public Guid PlantDiseaseId { get; set; }
        public string? Description { get; set; }
        public string? Feedback { get; set; }
        public string? Location { get; set; }
        public Guid? CreateBy { get; set; }

        public Guid? LandId { get; set; }
    }

    public class CreateDiseaseDiagnosesCommandHandler : IRequestHandler<CreateDiseaseDiagnosesCommand, DiseaseDiagnosesDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseDiagnosis> _repo;
        private readonly IUnitOfWork<DiseaseContext> _context;
        private IMapper _mapper;
        private ILogger<CreateDiseaseDiagnosesCommandHandler> _logger;

        public CreateDiseaseDiagnosesCommandHandler(IUnitOfWork<DiseaseContext> context, ISQLRepository<DiseaseContext, DiseaseDiagnosis> repo,
            IMapper mapper, ILogger<CreateDiseaseDiagnosesCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DiseaseDiagnosesDTO> Handle(CreateDiseaseDiagnosesCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Starting insert...");
            try
            {
                /*var disease = _mapper.Map<DiseaseDiagnosesDTO>(request);*/
                DiseaseDiagnosesDTO disease = new DiseaseDiagnosesDTO
                {
                    PlantDiseaseId = request.PlantDiseaseId,
                    Description = request.Description,
                    Feedback = request.Feedback,
                    Location = request.Location,
                    CreateBy = request.CreateBy,
                    LandId = request.LandId
                };
                await _repo.AddAsync(disease);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<DiseaseDiagnosesDTO>(null);
                }
                return disease;
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End insert...");
                throw new NotFoundException("Insert fail!");
            }



        }
    }
}
