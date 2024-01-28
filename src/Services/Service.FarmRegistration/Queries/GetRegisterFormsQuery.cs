using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmRegistry.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.Registration.Queries
{
    public class GetRegisterFormsQuery: IRequest<PagedList<RegisterFormResponse>>
    {
        public PaginationRequest Pagination { get; set; } = new();
    }

    public class GetRegisterFormsQueryHandler : IRequestHandler<GetRegisterFormsQuery, PagedList<RegisterFormResponse>>
    {
        private readonly ISQLRepository<RegistrationContext, FarmRegistration> _repo;
        private IMapper _mapper;

        public GetRegisterFormsQueryHandler(IMapper mapper, 
            ISQLRepository<RegistrationContext, FarmRegistration> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


        public Task<PagedList<RegisterFormResponse>> Handle(GetRegisterFormsQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo
                .GetMany(null
                , e=>e.Include(r=>r.Solution)).Result!
                .OrderBy(x => x.FirstName)
                .ToList();

            return Task.FromResult(
                PagedList<RegisterFormResponse>.ToPagedList(
                    _mapper.Map<List<RegisterFormResponse>>(rs),
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize
                ));
        }
    }
}
