using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.RefFertilizes
{
    public class UpdateRefFertilizeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public RefFertilizeRequest Fertilize { get; set; }
    }

    public class UpdateRefFertilizeCommandHandler : IRequestHandler<UpdateRefFertilizeCommand, Guid>
    {
        private ISQLRepository<FarmFertilizeContext, ReferencedFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateRefFertilizeCommandHandler> _logger;

        public UpdateRefFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, ReferencedFertilize> fertilizes,
            IMapper mapper,
            ILogger<UpdateRefFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(UpdateRefFertilizeCommand request, CancellationToken cancellationToken)
        {
            var item = await _fertilizes.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Fertilize, item);

            await _fertilizes.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }

}
