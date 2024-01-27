using AutoMapper;
using Infrastructure.Identity.Contexts;
using MediatR;
using Service.Identity.DTOs;
using SharedApplication.Authorize.Contracts;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Identity.Queries
{
    public class CheckValidAccountQuery: IRequest<bool>
    {
        public string Email { get; set; }
    }

    public class CheckValidAccountQueryHandler : IRequestHandler<CheckValidAccountQuery, bool>
    {
        private IIdentityService _identity;



        private ISQLRepository<IdentityContext, Site> _sites;
        private IUnitOfWork<IdentityContext> _unitOfWork;
        private IMapper _mapper;
        private ILogger<CheckValidAccountQueryHandler> _logger;

        public CheckValidAccountQueryHandler(IIdentityService identity,
            ISQLRepository<IdentityContext, Site> sites,
            IUnitOfWork<IdentityContext> unitOfWork,
            IMapper mapper,
            ILogger<CheckValidAccountQueryHandler> logger)
        {
            _identity = identity;
            _sites = sites;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<bool> Handle(CheckValidAccountQuery request, CancellationToken cancellationToken)
        {
            var user = _identity.FindAccount(e => e.Email == request.Email);

            if (user == null)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);

        }
    }
}
