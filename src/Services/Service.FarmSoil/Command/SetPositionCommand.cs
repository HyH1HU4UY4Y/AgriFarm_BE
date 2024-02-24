using AutoMapper;
using Infrastructure.Soil.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Soil.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Soil.Command
{
    public class SetPositionCommand: IRequest<LandResponse>
    {
        public Guid SiteId { get; set; }
        public Guid LandId { get; set; }
        public List<PositionPoint> Positions { get; set; } = new();
    }

    public class SetPositionCommandHandler : IRequestHandler<SetPositionCommand, LandResponse>
    {

        private readonly ISQLRepository<FarmSoilContext, FarmSoil> _lands;
        private readonly IUnitOfWork<FarmSoilContext> _unit;
        private readonly IMapper _mapper;
        private readonly ILogger<SetPositionCommandHandler> _logger;

        public SetPositionCommandHandler(IUnitOfWork<FarmSoilContext> context,
            ISQLRepository<FarmSoilContext, FarmSoil> lands,
            IMapper mapper,
            ILogger<SetPositionCommandHandler> logger)
        {
            _unit = context;
            _lands = lands;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LandResponse> Handle(SetPositionCommand request, CancellationToken cancellationToken)
        {
            var land = await _lands.GetOne(
                e => e.SiteId == request.SiteId && e.Id == request.LandId,
                ls => ls.Include(x=>x.Site)
                );
            if (land == null)
            {
                throw new NotFoundException("Land not exist");
            }

            if (request.Positions.Any())
            {
                land.Positions = request.Positions;

                await _lands.UpdateAsync(land);

                await _unit.SaveChangesAsync(cancellationToken);
            }



            return _mapper.Map<LandResponse>(land);
        }
    }
}
