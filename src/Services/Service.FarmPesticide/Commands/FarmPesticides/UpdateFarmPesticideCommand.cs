using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class UpdateFarmPesticideCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public PesticideRequest Pesticide { get; set; }
    }

    public class UpdateFarmPesticideCommandHandler : IRequestHandler<UpdateFarmPesticideCommand, Guid>
    {
        private ISQLRepository<FarmPesticideContext, FarmPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateFarmPesticideCommandHandler> _logger;

        public UpdateFarmPesticideCommandHandler(ISQLRepository<FarmPesticideContext, FarmPesticide> pesticides,
            IMapper mapper,
            ILogger<UpdateFarmPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(UpdateFarmPesticideCommand request, CancellationToken cancellationToken)
        {
            var item = await _pesticides.GetOne(e => e.Id == request.Id);

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Pesticide, item);

            await _pesticides.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
