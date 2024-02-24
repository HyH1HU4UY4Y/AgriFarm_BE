using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Newtonsoft.Json;
using Service.Pesticide.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class AddFarmPesticideCommand : IRequest<PesticideResponse>
    {
        public PesticideCreateRequest Pesticide { get; set; }
        public Guid SiteId { get; set; }
    }

    public class AddFarmPesticideCommandHandler : IRequestHandler<AddFarmPesticideCommand, PesticideResponse>
    {
        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<AddFarmPesticideCommandHandler> _logger;

        public AddFarmPesticideCommandHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<AddFarmPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PesticideResponse> Handle(AddFarmPesticideCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                //- check for super admin
                - check integrated with ref Pesticide info
            */

            var item = _mapper.Map<FarmPesticide>(request.Pesticide);
            item.SiteId = request.SiteId;

            await _pesticides.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PesticideResponse>(item);
        }
    }
}
