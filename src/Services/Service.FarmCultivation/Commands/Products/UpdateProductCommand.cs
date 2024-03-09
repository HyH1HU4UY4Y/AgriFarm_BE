using AutoMapper;
using Infrastructure.FarmCultivation.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.FarmCultivation.DTOs.Products;
using SharedDomain.Entities.Schedules;
using SharedDomain.Entities.Schedules.Cultivations;
using SharedDomain.Exceptions;
using SharedDomain.Repositories.Base;

namespace Service.FarmCultivation.Commands.Products
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public Guid Id { get; set; }
        public ProductRequest Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private ISQLRepository<CultivationContext, HarvestProduct> _products;
        private ISQLRepository<CultivationContext, CultivationSeason> _seasons;
        private IMapper _mapper;
        private IUnitOfWork<CultivationContext> _unit;
        private ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(ISQLRepository<CultivationContext, HarvestProduct> products,
            IMapper mapper,
            IUnitOfWork<CultivationContext> unit,
            ILogger<UpdateProductCommandHandler> logger,
            ISQLRepository<CultivationContext, CultivationSeason> seasons)
        {
            _products = products;
            _mapper = mapper;
            _unit = unit;
            _logger = logger;
            _seasons = seasons;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var item = await _products.GetOne(e => e.Id == request.Id
                                                && !e.Season.IsDeleted,
                                                ls => ls.Include(x => x.Season));

            if (item == null)
            {
                throw new NotFoundException("Item not exist");
            }

            _mapper.Map(request.Product, item);

            await _products.UpdateAsync(item);
            await _unit.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(item);
        }
    }
}
