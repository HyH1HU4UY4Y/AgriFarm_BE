using AutoMapper;
using Infrastructure.RiskAssessment.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.RiskAssessment.DTOs;
using SharedDomain.Entities.Risk;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.RiskAssessment.Queries
{
    public class GetRiskMappingDetailQuery: IRequest<RiskMappingResponse>
    {
        public Guid taskId { get; set; }
    }

    public class GetRiskMappingDetailQueryHandler : IRequestHandler<GetRiskMappingDetailQuery, RiskMappingResponse>
    {
        private ISQLRepository<RiskAssessmentContext, RiskMapping> _repo;
        private readonly IMapper _mapper;
        public GetRiskMappingDetailQueryHandler(IMapper mapper, ISQLRepository<RiskAssessmentContext, RiskMapping> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<RiskMappingResponse> Handle(GetRiskMappingDetailQuery request, CancellationToken cancellationToken)
        {
            var rs = await _repo.GetOne(r => r.TaskId == request.taskId,
                                        ls=>ls.Include(x=>x.RiskMaster!));
            if (rs == null)
            {
                throw new NotFoundException();
            }
            return _mapper.Map<RiskMappingResponse>(rs);
        }
    }
}
