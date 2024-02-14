using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Commands
{
    public class UpdateFeedbackCommand : IRequest<DiseaseDiagnosesDTO>
    {
        public Guid Id { get; set; }
        public string? Feedback { get; set; }
    }

    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, DiseaseDiagnosesDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseDiagnosis> _disease;
        private readonly IUnitOfWork<DiseaseContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFeedbackCommandHandler> _logger;


        public UpdateFeedbackCommandHandler(IMapper mapper,
            IUnitOfWork<DiseaseContext> unitOfWork,
            ISQLRepository<DiseaseContext, DiseaseDiagnosis> disease,
            ILogger<UpdateFeedbackCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _disease = disease;
            _logger = logger;
        }

        public async Task<DiseaseDiagnosesDTO> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _disease.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    item.Feedback = request.Feedback;
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
