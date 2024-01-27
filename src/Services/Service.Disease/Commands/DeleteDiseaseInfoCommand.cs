using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Commands
{
    public class DeleteDiseaseInfoCommand : IRequest<DiseaseInfoDTO>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDiseaseCommandHandler : IRequestHandler<DeleteDiseaseInfoCommand, DiseaseInfoDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseInfo> _disease;
        private readonly IUnitOfWork<DiseaseContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDiseaseCommandHandler> _logger;


        public DeleteDiseaseCommandHandler(IMapper mapper,
            IUnitOfWork<DiseaseContext> unitOfWork,
            ISQLRepository<DiseaseContext, DiseaseInfo> disease,
            ILogger<DeleteDiseaseCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _disease = disease;
            _logger = logger;
        }

        public async Task<DiseaseInfoDTO> Handle(DeleteDiseaseInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting delete...");
            try
            {
                var item = await _disease.GetOne(e => e.Id == request.Id && e.IsDeleted == false);
                if (item != null)
                {
                    await _disease.SoftDeleteAsync(item);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("End delete...");
                }
                return _mapper.Map<DiseaseInfoDTO>(item);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End delete...");
                throw new NotFoundException("Delete fail!");
            }
        }
    }
}
