using SharedApplication.CQRS;
using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using Service.FarmRegistry.DTOs;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using Infrastructure.Registration.Repositories;
using SharedDomain.Exceptions;

namespace Service.FarmRegistry.Commands
{
    public record RegistFarmCommand
        (string Name, string Content, string Phone
        , string Email, string Address, string SiteKey
        , string SiteName, Guid SolutionId
        , string PaymentDetail) :IRequest<RegisterFormResponse>;
        
    public class RegistFarmCommandhandler: IRequestHandler<RegistFarmCommand, RegisterFormResponse>
    {
        private readonly ICommandRepository<RegistrationContext, FarmRegistration> _resRepo;
        private readonly ISolutionQueryRepo _solRepo;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;

        public RegistFarmCommandhandler(IMapper mapper,
            IUnitOfWork<RegistrationContext> unitOfWork,
            ICommandRepository<RegistrationContext, FarmRegistration> resRepo,
            ISolutionQueryRepo solRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _resRepo = resRepo;
            _solRepo = solRepo;
        }

        public Task<RegisterFormResponse> Handle(RegistFarmCommand request, CancellationToken cancellationToken)
        {
            var solution = _solRepo.All.FirstOrDefault(e => e.Id == request.SolutionId);
            if (solution == null) throw new NotFoundException("Solution not exist!");

            var entity = _mapper.Map<FarmRegistration>(request);
            entity.Cost = solution.Price;
            _resRepo.AddAsync(entity);

            _unitOfWork.SaveChangesAsync(cancellationToken);

            return Task.FromResult(_mapper.Map<RegisterFormResponse>(entity));
        }
    }
}
