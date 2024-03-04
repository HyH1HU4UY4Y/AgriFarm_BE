using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Entities.Schedules;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Service.FarmCultivation.Commands.Products
{
    public class HarvestProductCommand: IRequest
    {
        public Guid Id { get; set; }
        public HarvestRequest Harvest {  get; set; }
    }

    public class HarvestProductCommandHandler : IRequestHandler<HarvestProductCommand, Unit>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<HarvestProductCommandHandler> _logger;

        public HarvestProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<HarvestProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<Unit> Handle(HarvestProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id
                                                //&& !e.Season.IsDeleted,
                                                //ls => ls.Include(x => x.Season)
                                                );

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }
            /*
            TODO:
                - Process measure unit
            */

            item.TotalQuantity = request.Harvest.TotalQuantity;
            item.Quantity = request.Harvest.TotalQuantity;
            item.Unit = request.Harvest.Unit;
            item.HarvestTime = DateTime.Now;

            await _products.UpdateAsync(item);
            await _unit.SaveChangesAsync();

            return Unit.Value; 
                //_mapper.Map<ProductResponse>(item);
        }
    }
}
