using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Disease.Commands
{
    public class UpdateDiseaseInfoCommand : IRequest<DiseaseInfoDTO>
    {
        public Guid Id { get; set; }
        public string? DiseaseName { get; set; }
        [StringLength(8000)]
        public string? Symptoms { get; set; }
        [StringLength(8000)]
        public string? Cause { get; set; }
        public string? PreventiveMeasures { get; set; }
        public string? Suggest { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }

    }

    public class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseInfoCommand, DiseaseInfoDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseInfo> _disease;
        private readonly IUnitOfWork<DiseaseContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDiseaseCommandHandler> _logger;


        public UpdateDiseaseCommandHandler(IMapper mapper,
            IUnitOfWork<DiseaseContext> unitOfWork,
            ISQLRepository<DiseaseContext, DiseaseInfo> disease,
            ILogger<UpdateDiseaseCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _disease = disease;
            _logger = logger;
        }

        public async Task<DiseaseInfoDTO> Handle(UpdateDiseaseInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting update...");
            try
            {
                var item = await _disease.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    item.DiseaseName = request.DiseaseName!;
                    item.Symptoms = request.Symptoms!;
                    item.Cause = request.Cause!;
                    item.PreventiveMeasures = request.PreventiveMeasures!;
                    item.Suggest = request.Suggest!;
                    item.UpdateBy = request.UpdateBy;
                    await _disease.UpdateAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End update...");
                }
                return _mapper.Map<DiseaseInfoDTO>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End update...");
                throw new NotFoundException("Update fail!");
            }
        }
    }
}
