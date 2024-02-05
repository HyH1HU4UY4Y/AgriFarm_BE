using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Service.FarmSite.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands.Farms
{
    public class DeleteFarmCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public SiteEditRequest Site { get; set; }

    }

    public class DeleteFarmCommandHandler : IRequestHandler<DeleteFarmCommand, Guid>
    {

        private readonly ISQLRepository<SiteContext, Site> _sites;
        private readonly IUnitOfWork<SiteContext> _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFarmCommandHandler> _logger;

        public DeleteFarmCommandHandler(IUnitOfWork<SiteContext> context,
            ISQLRepository<SiteContext, Site> sites,
            IMapper mapper,
            ILogger<DeleteFarmCommandHandler> logger)
        {
            _context = context;
            _sites = sites;
            _mapper = mapper;
            _logger = logger;
        }



        public async Task<Guid> Handle(DeleteFarmCommand request, CancellationToken cancellationToken)
        {
            var item = await _sites.GetOne(e => e.Id == request.Id);


            await _sites.SoftDeleteAsync(item);
            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
