using AutoMapper;
using Infrastructure.Disease.Context;
using MediatR;
using Service.Disease.DTOs;
using SharedDomain.Entities.Diagnosis;
using SharedDomain.Repositories.Base;

namespace Service.Disease.Queries
{
    public class GetDiseaseInfoByIdQuery : IRequest<DiseaseInfoDTO>
    {
        public Guid DiseaseId { get; set; }
    }

    public class GetDiseaseByIdQueryHandler : IRequestHandler<GetDiseaseInfoByIdQuery, DiseaseInfoDTO>
    {

        private ISQLRepository<DiseaseContext, DiseaseInfo> _repo;
        private readonly IMapper _mapper;

        public GetDiseaseByIdQueryHandler(IMapper mapper, ISQLRepository<DiseaseContext, DiseaseInfo> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<DiseaseInfoDTO> Handle(GetDiseaseInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(e => e.Id == request.DiseaseId);

            return _mapper.Map<DiseaseInfoDTO>(rs);

        }
    }
    
}

