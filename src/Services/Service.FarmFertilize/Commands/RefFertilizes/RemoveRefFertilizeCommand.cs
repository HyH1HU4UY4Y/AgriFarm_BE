using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.RefFertilizes
{
    public class RemoveRefFertilizeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class RemoveRefFertilizeCommandHandler : IRequestHandler<RemoveRefFertilizeCommand, Guid>
    {
        private ISQLRepository<FarmFertilizeContext, ReferencedFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<RemoveRefFertilizeCommandHandler> _logger;

        public RemoveRefFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, ReferencedFertilize> fertilizes,
            IMapper mapper,
            ILogger<RemoveRefFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(RemoveRefFertilizeCommand request, CancellationToken cancellationToken)
        {

            /*
            TODO:
                - switch between soft and raw
            */

            var item = await _fertilizes.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _fertilizes.SoftDeleteAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
