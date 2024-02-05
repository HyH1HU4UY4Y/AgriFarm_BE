using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.RefFertilizes
{
    public class AddRefFertilizeCommand : IRequest<Guid>
    {
        public RefFertilizeRequest Fertilize { get; set; }
    }

    public class AddRefFertilizeCommandHandler : IRequestHandler<AddRefFertilizeCommand, Guid>
    {
        private ISQLRepository<FarmFertilizeContext, ReferencedFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<AddRefFertilizeCommandHandler> _logger;

        public AddRefFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, ReferencedFertilize> fertilizes,
            IMapper mapper,
            ILogger<AddRefFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddRefFertilizeCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for each bussiness role
                - check integrated with ref Fertilize info
            */

            var item = _mapper.Map<ReferencedFertilize>(request.Fertilize);

            await _fertilizes.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
