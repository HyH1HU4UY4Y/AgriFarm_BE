using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Commands.Products
{
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<DeleteProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id 
                                                && !e.Season.IsDeleted,
                                                ls => ls.Include(x=>x.Season));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            await _products.SoftDeleteAsync(item);
            await _unit.SaveChangesAsync();

            return item.Id;

        }
    }
}
