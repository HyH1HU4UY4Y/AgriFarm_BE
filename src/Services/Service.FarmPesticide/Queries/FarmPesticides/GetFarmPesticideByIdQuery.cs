using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Queries.FarmPesticides
{
    public class GetFarmPesticideByIdQuery : IRequest<PesticideResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetFarmPesticideByIdQueryHandler : IRequestHandler<GetFarmPesticideByIdQuery, PesticideResponse>
    {

        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<GetFarmPesticideByIdQueryHandler> _logger;

        public GetFarmPesticideByIdQueryHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<GetFarmPesticideByIdQueryHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<PesticideResponse> Handle(GetFarmPesticideByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _pesticides.GetOne(e => e.Id == request.Id, ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<PesticideResponse>(item);
        }
    }
}
