using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Commands.FarmFertilizes
{
    public class UpdateFarmFertilizeCommand : IRequest<FertilizeResponse>
    {
        public Guid Id { get; set; }
        public FertilizeInfoRequest Fertilize { get; set; }
    }

    public class UpdateFarmFertilizeCommandHandler : IRequestHandler<UpdateFarmFertilizeCommand, FertilizeResponse>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmFertilizeCommandHandler> _logger;

        public UpdateFarmFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<UpdateFarmFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<FertilizeResponse> Handle(UpdateFarmFertilizeCommand request, CancellationToken cancellationToken)
        {
            var item = await _fertilizes.GetOne(e => e.Id == request.Id,
                                ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Fertilize, item);

            await _fertilizes.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FertilizeResponse>(item);
        }
    }
}
