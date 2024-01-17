using SharedApplication.CQRS;
using AutoMapper;
using Infrastructure.FarmRegistry.Contexts;
using MediatR;
using SharedDomain.Defaults;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.FarmRegistry.Commands
{
    public record ResolveFormCommand: ICommand<Unit>
    {
        public Guid Id { get; set; }
        public DecisonOption Decison { get; set; }
        [StringLength(3000)]
        public string Notes { get; set; }
    }

    public class ResolveFormCommandHandler : ICommandHandler<ResolveFormCommand, Unit>
    {
        private readonly ISQLRepository<RegistrationContext, FarmRegistration> _command;
        private readonly IUnitOfWork<RegistrationContext> _unitOfWork;
        private readonly IMapper _mapper;

        public ResolveFormCommandHandler(IMapper mapper, IUnitOfWork<RegistrationContext> unitOfWork, ISQLRepository<RegistrationContext, FarmRegistration> command)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _command = command;
        }

        public Task<Unit> Handle(ResolveFormCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
