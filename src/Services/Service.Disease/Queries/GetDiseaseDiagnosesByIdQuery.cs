using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Queries
{
    public class GetDiseaseDiagnosesByIdQuery : IRequest<DiseaseDiagnosesDTO>
    {
        public Guid DiseaseDiagnosesId { get; set; }
    }
    public class GetDiseaseDiagnosesByIdQueryHandler : IRequestHandler<GetDiseaseDiagnosesByIdQuery, DiseaseDiagnosesDTO>
    {
        private ISQLRepository<DiseaseContext, DiseaseDiagnosis> _repo;
        private readonly IMapper _mapper;

        public GetDiseaseDiagnosesByIdQueryHandler(IMapper mapper, ISQLRepository<DiseaseContext, DiseaseDiagnosis> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<DiseaseDiagnosesDTO> Handle(GetDiseaseDiagnosesByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => (e.Id == request.DiseaseDiagnosesId && e.IsDeleted == false), r => r.Include(e => e.PlantDisease!));

            return _mapper.Map<DiseaseDiagnosesDTO>(rs);
        }
    }
}
