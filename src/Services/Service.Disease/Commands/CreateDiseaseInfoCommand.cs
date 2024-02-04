using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Disease.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Defaults;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.Disease.Commands
{
    public class CreateDiseaseInfoCommand : IRequest<DiseaseInfoDTO>
    {
        public string? DiseaseName { get; set; }
        [StringLength(8000)]
        public string? Symptoms { get; set; }
        [StringLength(8000)]
        public string? Cause { get; set; }
        public string? PreventiveMeasures { get; set; }
        public string? Suggest { get; set; }
        public Guid? CreateBy { get; set; }

    }

    public class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseInfoCommand, DiseaseInfoDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseInfo> _repo;
        private readonly IUnitOfWork<DiseaseContext> _context;
        private IMapper _mapper;
        private ILogger<CreateDiseaseCommandHandler> _logger;

        public CreateDiseaseCommandHandler(IUnitOfWork<DiseaseContext> context, ISQLRepository<DiseaseContext, DiseaseInfo> repo,
            IMapper mapper, ILogger<CreateDiseaseCommandHandler> logger)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DiseaseInfoDTO> Handle(CreateDiseaseInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting insert...");
            try
            {
                DiseaseInfoDTO disease = new DiseaseInfoDTO
                {
                    DiseaseName = request.DiseaseName!,
                    Symptoms = request.Symptoms!,
                    Cause = request.Cause!,
                    PreventiveMeasures = request.PreventiveMeasures!,
                    Suggest = request.Suggest!,
                    CreateBy = request.CreateBy
                };
                await _repo.AddAsync(disease);
                var rs = _context.SaveChangesAsync(cancellationToken).Result > 0;
                _logger.LogInformation("End insert...");
                if (!rs)
                {
                    return _mapper.Map<DiseaseInfoDTO>(null);
                }
                return disease;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation("End insert...");
                throw new NotFoundException("Insert fail!");
            }
        }
    }
}
