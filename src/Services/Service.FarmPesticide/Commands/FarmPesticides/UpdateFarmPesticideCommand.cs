using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.FarmPesticides
{
    public class UpdateFarmPesticideCommand : IRequest<PesticideResponse>
    {
        public Guid Id { get; set; }
        public PesticideInfoRequest Pesticide { get; set; }
    }

    public class UpdateFarmPesticideCommandHandler : IRequestHandler<UpdateFarmPesticideCommand, PesticideResponse>
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

        public async Task<PesticideResponse> Handle(UpdateFarmPesticideCommand request, CancellationToken cancellationToken)
        {
            var item = await _pesticides.GetOne(e => e.Id == request.Id,
                                                ls => ls.Include(x=>x.Properties));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Pesticide, item);

            await _pesticides.UpdateAsync(item);

            await _unit.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PesticideResponse>(item);
        }
    }
}
