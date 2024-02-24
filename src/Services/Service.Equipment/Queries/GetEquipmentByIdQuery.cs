using AutoMapper;
using Infrastructure.Equipment.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Equipment.DTOs;
using SharedApplication.Pagination;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Queries
{
    public class GetEquipmentByIdQuery : IRequest<EquipmentResponse>
    {
        public Guid Id { get; set; }

    }

    public class GetEquipmentByIdQueryHandler : IRequestHandler<GetEquipmentByIdQuery, EquipmentResponse>
    {

        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<GetEquipmentByIdQueryHandler> _logger;

        public GetEquipmentByIdQueryHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<GetEquipmentByIdQueryHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<EquipmentResponse> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _equipments.GetOne(e => e.Id == request.Id, 
                                                ls => ls.Include(x => x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            return _mapper.Map<EquipmentResponse>(item);
        }
    }
}
