using AutoMapper;
using Infrastructure.Registration.Repositories;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.PostHarvest;

namespace Service.Registration.Queries
{
    public class GetSolutionsQuery: IRequest<List<SolutionResponse>>
    {
    }

    public class GetSolutionsQueryHandler : IRequestHandler<GetSolutionsQuery, List<SolutionResponse>>
    {
        private ISolutionQueryRepo _repo;
        private IMapper _mapper;

        public GetSolutionsQueryHandler(ISolutionQueryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<List<SolutionResponse>> Handle(GetSolutionsQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.All
                .OrderBy (x => x.Name)
                .ToList();

            return Task.FromResult(_mapper.Map<List<SolutionResponse>>(rs));
        }
    }
}
