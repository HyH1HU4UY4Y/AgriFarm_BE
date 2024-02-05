using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.RefPesticides
{
    public class AddRefPesticideCommand : IRequest<Guid>
    {
        public RefPesticideRequest Pesticide { get; set; }
    }

    public class AddRefPesticideCommandHandler : IRequestHandler<AddRefPesticideCommand, Guid>
    {
        private ISQLRepository<FarmPesticideContext, ReferencedPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<AddRefPesticideCommandHandler> _logger;

        public AddRefPesticideCommandHandler(ISQLRepository<FarmPesticideContext, ReferencedPesticide> pesticides,
            IMapper mapper,
            ILogger<AddRefPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddRefPesticideCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for each bussiness role
                - check integrated with ref Pesticide info
            */

            var item = _mapper.Map<ReferencedPesticide>(request.Pesticide);

            await _pesticides.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
