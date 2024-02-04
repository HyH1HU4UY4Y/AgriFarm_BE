using AutoMapper;
using Infrastructure.Pesticide.Contexts;
using MediatR;
using Service.Pesticide.DTOs;
using SharedDomain.Entities.FarmComponents.Common;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.Pesticide.Commands.RefPesticides
{
    public class UpdateRefPesticideCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public RefPesticideRequest Pesticide { get; set; }
    }

    public class UpdateRefPesticideCommandHandler : IRequestHandler<UpdateRefPesticideCommand, Guid>
    {
        private ISQLRepository<FarmPesticideContext, ReferencedPesticide> _pesticides;
        private IUnitOfWork<FarmPesticideContext> _unit;
        private IMapper _mapper;
        private ILogger<UpdateRefPesticideCommandHandler> _logger;

        public UpdateRefPesticideCommandHandler(ISQLRepository<FarmPesticideContext, ReferencedPesticide> pesticides,
            IMapper mapper,
            ILogger<UpdateRefPesticideCommandHandler> logger,
            IUnitOfWork<FarmPesticideContext> unit)
        {
            _pesticides = pesticides;
            _mapper = mapper;
            _logger = logger;
            _unit = unit;
        }

        public async Task<Guid> Handle(UpdateRefPesticideCommand request, CancellationToken cancellationToken)
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
