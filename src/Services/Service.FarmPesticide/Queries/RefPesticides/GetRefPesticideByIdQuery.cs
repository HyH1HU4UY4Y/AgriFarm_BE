using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Queries.RefPesticides
{
    public class GetRefPesticideByIdQuery: IRequest<RefPesticideResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetRefPesticideByIdQueryHandler : IRequestHandler<GetRefPesticideByIdQuery, RefPesticideResponse>
    {
        private ISQLRepository<FarmPesticideContext, ReferencedPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<GetRefPesticideByIdQueryHandler> _logger;

        public GetRefPesticideByIdQueryHandler(ISQLRepository<FarmPesticideContext, ReferencedPesticide> pesticides,
            IMapper mapper,
            ILogger<GetRefPesticideByIdQueryHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }


        public async Task<RefPesticideResponse> Handle(GetRefPesticideByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _pesticides.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist!");
            }


            return _mapper.Map<RefPesticideResponse>(item);
        }
    }

}
