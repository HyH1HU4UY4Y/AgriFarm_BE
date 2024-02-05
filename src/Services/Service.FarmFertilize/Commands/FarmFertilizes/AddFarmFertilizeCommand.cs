using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Newtonsoft.Json;
using Service.Fertilize.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace Service.Fertilize.Commands.FarmFertilizes
{
    public class AddFarmFertilizeCommand : IRequest<Guid>
    {
        public FertilizeRequest Fertilize { get; set; }
    }

    public class AddFarmFertilizeCommandHandler : IRequestHandler<AddFarmFertilizeCommand, Guid>
    {
        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<AddFarmFertilizeCommandHandler> _logger;

        public AddFarmFertilizeCommandHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<AddFarmFertilizeCommandHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(AddFarmFertilizeCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                - check for super admin
                - check integrated with ref Fertilize info
            */

            var item = _mapper.Map<FarmFertilize>(request.Fertilize);

            await _fertilizes.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
