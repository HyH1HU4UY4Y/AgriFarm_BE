using AutoMapper;
using Infrastructure.Equipment.Contexts;
using MediatR;
using Service.Equipment.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules.Training;
using SharedDomain.Repositories.Base;

namespace Service.Equipment.Commands
{
    public class AddEquipmentCommand: IRequest<EquipmentResponse>
    {
        public EquipmentRequest Equipment {  get; set; }
        public Guid SiteId { get; set; }

    }

    public class AddEquipmentCommandHandler : IRequestHandler<AddEquipmentCommand, EquipmentResponse>
    {
        private ISQLRepository<FarmEquipmentContext, FarmEquipment> _equipments;
        private IUnitOfWork<FarmEquipmentContext> _unit;
        private IMapper _mapper;
        private ILogger<AddEquipmentCommandHandler> _logger;

        public AddEquipmentCommandHandler(ISQLRepository<FarmEquipmentContext, FarmEquipment> equipments,
            IMapper mapper,
            ILogger<AddEquipmentCommandHandler> logger,
            IUnitOfWork<FarmEquipmentContext> unit)
        {
            _equipments = equipments;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<EquipmentResponse> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
        {
            /*TODO:
                //- check for super admin
                
            */

            var item = _mapper.Map<FarmEquipment>(request.Equipment);
            item.SiteId = request.SiteId;

            await _equipments.AddAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EquipmentResponse>(item);
        }
    }
}
