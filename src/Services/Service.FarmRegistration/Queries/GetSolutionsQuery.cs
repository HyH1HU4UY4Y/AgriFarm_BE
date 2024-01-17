using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.Registration.Queries
{
    public class GetSolutionsQuery: IRequest<List<SolutionResponse>>
    {
    }

    public class GetSolutionsQueryHandler : IRequestHandler<GetSolutionsQuery, List<SolutionResponse>>
    {
        private ISQLRepository<RegistrationContext, PackageSolution> _repo;
        private readonly IMapper _mapper;

        public GetSolutionsQueryHandler(IMapper mapper, 
            ISQLRepository<RegistrationContext, PackageSolution> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public Task<List<SolutionResponse>> Handle(GetSolutionsQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.GetMany().Result!
                .OrderBy (x => x.Name)
                .ToList();

            return Task.FromResult(_mapper.Map<List<SolutionResponse>>(rs));
        }
    }
}
