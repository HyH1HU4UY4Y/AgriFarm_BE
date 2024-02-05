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
    public class AddFarmPesticideCommand : IRequest<Guid>
    {
        public PesticideRequest Pesticide { get; set; }
    }

    public class AddFarmPesticideCommandHandler : IRequestHandler<AddFarmPesticideCommand, Guid>
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

        public async Task<Guid> Handle(AddFarmPesticideCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with ref Pesticide info
            */

            var item = _mapper.Map<FarmPesticide>(request.Pesticide);

            await _pesticides.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
