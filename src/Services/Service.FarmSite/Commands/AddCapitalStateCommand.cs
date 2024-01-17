using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands
{
    public class AddCapitalStateCommand: IRequest<Guid>
    {
        public Guid SiteId { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; } = "none";
    }

    public class AddCapitalStateCommandHandler : IRequestHandler<AddCapitalStateCommand, Guid>
    {
        private readonly ISQLRepository<SiteContext, CapitalState> _caps;
        private readonly IUnitOfWork<SiteContext> _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AddCapitalStateCommand> _logger;

        public AddCapitalStateCommandHandler(
            IUnitOfWork<SiteContext> context,
            IMapper mapper,
            ILogger<AddCapitalStateCommand> logger,
            ISQLRepository<SiteContext, CapitalState> cap)
        {

            _context = context;
            _mapper = mapper;
            _logger = logger;
            _caps = cap;
        }


        public async Task<Guid> Handle(AddCapitalStateCommand request, CancellationToken cancellationToken)
        {
            var cap = _mapper.Map<CapitalState>(request);

            await _caps.AddAsync(cap);
            var rs = _context.SaveChangesAsync(cancellationToken).Result>0;
            if (!rs)
            {

            }

            return cap.Id;

        }
    }
}
