using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands
{
    public class CreateNewFarmCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string SiteCode { get; set; }
        public bool IsActive { get; set; } = false;
    }

    public class CreateNewFarmCommandHandler : IRequestHandler<CreateNewFarmCommand, Guid>
    {
        private readonly ISQLRepository<SiteContext, Site> _sites;
        private readonly IUnitOfWork<SiteContext> _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateNewFarmCommandHandler> _logger;

        public CreateNewFarmCommandHandler(IUnitOfWork<SiteContext> context,
            ISQLRepository<SiteContext, Site> sites,
            IMapper mapper,
            ILogger<CreateNewFarmCommandHandler> logger)
        {
            _context = context;
            _sites = sites;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateNewFarmCommand request, CancellationToken cancellationToken)
        {
            var site = _mapper.Map<Site>(request);
            

            await _sites.AddAsync(site);
            var rs = _context.SaveChangesAsync(cancellationToken).Result >0;
            if (!rs)
            {
                throw new NotFoundException("Not Found!");
            }

            return site.Id;
        }
    }

}
