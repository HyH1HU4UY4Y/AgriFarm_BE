using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using SharedDomain.Exceptions;

namespace Service.FarmRegistry.Commands
{
    public record RegistFarmCommand
        (string Name, string? FirstName
        , string? LastName, string Phone
        , string Email, string Address, string SiteCode
        , string SiteName, Guid SolutionId
        , string PaymentDetail) :IRequest<RegisterFormResponse>;
        
    public class RegistFarmCommandhandler: IRequestHandler<RegistFarmCommand, RegisterFormResponse>
    {
        private readonly ISQLRepository<RegistrationContext, FarmRegistration> _resRepo;
        private readonly ISQLRepository<RegistrationContext, PackageSolution> _solRepo;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;

        public RegistFarmCommandhandler(IMapper mapper,
            IUnitOfWork<RegistrationContext> unitOfWork,
            ISQLRepository<RegistrationContext, FarmRegistration> resRepo
,
            ISQLRepository<RegistrationContext, PackageSolution> solRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _resRepo = resRepo;
            _solRepo = solRepo;
        }

        public Task<RegisterFormResponse> Handle(RegistFarmCommand request, CancellationToken cancellationToken)
        {
            
            var solution = _solRepo.GetOne(e => e.Id == request.SolutionId).Result;
            if (solution == null) throw new Exception("Solution not exist!");

            var entity = _mapper.Map<FarmRegistration>(request);
            entity.Cost = solution.Price;
            _resRepo.AddAsync(entity);

            _unitOfWork.SaveChangesAsync(cancellationToken);

            return Task.FromResult(_mapper.Map<RegisterFormResponse>(entity));
        }
    }
}
