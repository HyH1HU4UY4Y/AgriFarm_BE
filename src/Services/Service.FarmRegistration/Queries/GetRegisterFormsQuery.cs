using AutoMapper;
using Infrastructure.Registration.Repositories;
using MediatR;
using Service.FarmRegistry.DTOs;

namespace Service.Registration.Queries
{
    public class GetRegisterFormsQuery: IRequest<List<RegisterFormResponse>>
    {
    }

    public class GetRegisterFormsQueryHandler : IRequestHandler<GetRegisterFormsQuery, List<RegisterFormResponse>>
    {
        private IRegistryQueryRepo _repo;
        private IMapper _mapper;

        public GetRegisterFormsQueryHandler(IMapper mapper, IRegistryQueryRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


        Task<List<RegisterFormResponse>> IRequestHandler<GetRegisterFormsQuery, List<RegisterFormResponse>>.Handle(GetRegisterFormsQuery request, CancellationToken cancellationToken)
        {
            var rs = _repo.All
                .OrderBy(x => x.Name)
                .ToList();

            return Task.FromResult(_mapper.Map<List<RegisterFormResponse>>(rs));
        }
    }
}
