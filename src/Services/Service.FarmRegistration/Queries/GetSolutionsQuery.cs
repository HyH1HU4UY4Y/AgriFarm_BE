using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.Registration.Queries
{
    public class GetSolutionsQuery: IRequest<PagedList<SolutionResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetSolutionsQueryHandler : IRequestHandler<GetSolutionsQuery, PagedList<SolutionResponse>>
    {
        private ISQLRepository<RegistrationContext, PackageSolution> _repo;
        private readonly IMapper _mapper;

        public GetSolutionsQueryHandler(IMapper mapper, 
            ISQLRepository<RegistrationContext, PackageSolution> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public Task<PagedList<SolutionResponse>> Handle(GetSolutionsQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.GetMany().Result!
                .OrderBy (x => x.Name)
                .ToList();

            return Task.FromResult(
                PagedList<SolutionResponse>.ToPagedList(
                    _mapper.Map<List<SolutionResponse>>(rs),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                ));
        }
    }
}
