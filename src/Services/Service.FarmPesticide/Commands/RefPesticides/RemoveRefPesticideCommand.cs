using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.RefPesticides
{
    public class RemoveRefPesticideCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveRefPesticideCommandHandler : IRequestHandler<RemoveRefPesticideCommand, Guid>
    {
        private ISQLRepository<FarmPesticideContext, ReferencedPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveRefPesticideCommandHandler> _logger;

        public RemoveRefPesticideCommandHandler(ISQLRepository<FarmPesticideContext, ReferencedPesticide> pesticides,
            IMapper mapper,
            ILogger<RemoveRefPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveRefPesticideCommand request, CancellationToken cancellationToken)
        {

            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _pesticides.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _pesticides.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
