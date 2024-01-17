using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands
{
    public class CreateSubscriptBillCommand: IRequest<Guid>
    {

        public Guid SiteId { get; set; }
        public Guid SolutionId { get; set; }

        public decimal Price { get; set; }

        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }

    }

    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptBillCommand, Guid>
    {
        private readonly ISQLRepository<SiteContext, Subscripton> _bills;
        private readonly IUnitOfWork<SiteContext> _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSubscriptionCommandHandler> _logger;

        public CreateSubscriptionCommandHandler(IMapper mapper,
            IUnitOfWork<SiteContext> context,
            ISQLRepository<SiteContext, Subscripton> bills,
            ILogger<CreateSubscriptionCommandHandler> logger)
        {
            _mapper = mapper;
            _context = context;
            _bills = bills;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSubscriptBillCommand request, CancellationToken cancellationToken)
        {
            var bill = _mapper.Map<Subscripton>(request);

            await _bills.AddAsync(bill);
            var rs = _context.SaveChangesAsync().Result>0;
            if (!rs)
            {

            }

            return bill.Id;

        }
    }
}
