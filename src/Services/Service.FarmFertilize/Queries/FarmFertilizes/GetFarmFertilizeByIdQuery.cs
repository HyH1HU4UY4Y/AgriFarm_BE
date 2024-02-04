using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Queries.FarmFertilizes
{
    public class GetFarmFertilizeByIdQuery : IRequest<FertilizeResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetFarmFertilizeByIdQueryHandler : IRequestHandler<GetFarmFertilizeByIdQuery, FertilizeResponse>
    {

        private ISQLRepository<FarmFertilizeContext, FarmFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmFertilizeByIdQueryHandler> _logger;

        public GetFarmFertilizeByIdQueryHandler(ISQLRepository<FarmFertilizeContext, FarmFertilize> fertilizes,
            IMapper mapper,
            ILogger<GetFarmFertilizeByIdQueryHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<FertilizeResponse> Handle(GetFarmFertilizeByIdQuery request, CancellationToken cancellationToken)
        {
            var item = _fertilizes.GetOne(e => e.Id == request.Id, ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<FertilizeResponse>(item);
        }
    }
}
