using AutoMapper;
using Infrastructure.FarmSite.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmSite.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmSite.Commands.Farms
{
    public class SetPositionCommand: IRequest<FullSiteResponse>
    {
        public Guid SiteId { get; set; }
        public List<PositionPoint> Positions { get; set; } = new();
    }

    public class SetPositionCommandHandler : IRequestHandler<SetPositionCommand, FullSiteResponse>
    {

        private readonly ISQLRepository<SiteContext, Site> _sites;
        private readonly IUnitOfWork<SiteContext> _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<SetPositionCommandHandler> _logger;

        public SetPositionCommandHandler(IUnitOfWork<SiteContext> context,
            ISQLRepository<SiteContext, Site> sites,
            IMapper mapper,
            ILogger<SetPositionCommandHandler> logger)
        {
            _unit = context;
            _sites = sites;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FullSiteResponse> Handle(SetPositionCommand request, CancellationToken cancellationToken)
        {
            var site =  await _sites.GetOne(e=>e.Id == request.SiteId);
            if (site == null)
            {
                throw new NotFoundException("Site not exist");
            }

            if(request.Positions.Any())
            {
                site.Positions = request.Positions;

                await _sites.UpdateAsync(site);

                await _unit.SaveChangesAsync(cancellationToken);
            }

            

            return _mapper.Map<FullSiteResponse>(site);
        }
    }
}
