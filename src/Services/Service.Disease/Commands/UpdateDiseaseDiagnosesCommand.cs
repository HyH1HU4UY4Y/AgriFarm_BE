using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Defaults;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Commands
{
    public class UpdateDiseaseDiagnosesCommand : IRequest<DiseaseDiagnosesDTO>
    {
        public Guid Id { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; }
    }

    public class UpdateDiseaseDiagnosesCommandHandler : IRequestHandler<UpdateDiseaseDiagnosesCommand, DiseaseDiagnosesDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseDiagnosis> _disease;
        private readonly IUnitOfWork<DiseaseContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDiseaseCommandHandler> _logger;


        public UpdateDiseaseDiagnosesCommandHandler(IMapper mapper,
            IUnitOfWork<DiseaseContext> unitOfWork,
            ISQLRepository<DiseaseContext, DiseaseDiagnosis> disease,
            ILogger<UpdateDiseaseCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _disease = disease;
            _logger = logger;
        }

        public async Task<DiseaseDiagnosesDTO> Handle(UpdateDiseaseDiagnosesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _disease.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    item.FeedbackStatus = request.FeedbackStatus;
                    await _disease.UpdateAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End update...");
                }
                return _mapper.Map<DiseaseDiagnosesDTO>(item);
            } catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End update...");
                throw new NotFoundException("Update fail!");
            }
        }
    }
}
