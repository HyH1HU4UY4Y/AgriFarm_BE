using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Service.FarmSite.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands.Farms
{
    public class UpdateFarmCommand : IRequest<FullSiteResponse>
    {
        public Guid Id { get; set; }
        public SiteEditRequest Site {  get; set; }

    }

    public class UpdateFarmCommandHandler : IRequestHandler<UpdateFarmCommand, FullSiteResponse>
    {

        private readonly ISQLRepository<SiteContext, Site> _sites;
        private readonly IUnitOfWork<SiteContext> _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFarmCommandHandler> _logger;

        public UpdateFarmCommandHandler(IUnitOfWork<SiteContext> context,
            ISQLRepository<SiteContext, Site> sites,
            IMapper mapper,
            ILogger<UpdateFarmCommandHandler> logger)
        {
            _context = context;
            _sites = sites;
            _mapper = mapper;
            _logger = logger;
        }

        

        public async Task<FullSiteResponse> Handle(UpdateFarmCommand request, CancellationToken cancellationToken)
        {
            var item = await _sites.GetOne(e => e.Id == request.Id);

            if(item == null)
            {
                throw new NotFoundException("Site not exist");
            }

            _mapper.Map(request.Site, item);


            await _sites.UpdateAsync(item);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FullSiteResponse>(item);
        }
    }
}
