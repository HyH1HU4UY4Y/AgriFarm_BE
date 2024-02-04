using AutoMapper;
using Infrastructure.Fertilize.Contexts;
using MediatR;
using Service.Fertilize.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Fertilize.Queries.RefFertilizes
{
    public class GetRefFertilizeByIdQuery : IRequest<RefFertilizeResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetRefFertilizeByIdQueryHandler : IRequestHandler<GetRefFertilizeByIdQuery, RefFertilizeResponse>
    {
        private ISQLRepository<FarmFertilizeContext, ReferencedFertilize> _fertilizes;
        private IUnitOfWork<FarmFertilizeContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefFertilizeByIdQueryHandler> _logger;

        public GetRefFertilizeByIdQueryHandler(ISQLRepository<FarmFertilizeContext, ReferencedFertilize> fertilizes,
            IMapper mapper,
            ILogger<GetRefFertilizeByIdQueryHandler> logger,
            IUnitOfWork<FarmFertilizeContext> unit)
        {
            _fertilizes = fertilizes;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }


        public async Task<RefFertilizeResponse> Handle(GetRefFertilizeByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _fertilizes.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist!");
            }


            return _mapper.Map<RefFertilizeResponse>(item);
        }
    }

}
