using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;

namespace Infrastructure.Common.Replication.Commands
{
    public class SaveComponentCommand<T> : IRequest<Guid> where T : BaseComponent
    {
        public T Item { get; set; }
        public EventState State { get; set; }
    }

    public class SaveComponentCommandHandler<TComp, TDBContext> : IRequestHandler<SaveComponentCommand<TComp>, Guid> 
        where TComp : BaseComponent where TDBContext : DbContext
    {

        private ISQLRepository<TDBContext, BaseComponent> _components;
        private IUnitOfWork<TDBContext> _unit;
        private ILogger<SaveComponentCommandHandler<TComp, TDBContext>> _logger;
        private IMapper _mapper;

        public SaveComponentCommandHandler(ISQLRepository<TDBContext, BaseComponent> components,
            IUnitOfWork<TDBContext> unit,
            ILogger<SaveComponentCommandHandler<TComp, TDBContext>> logger,
            IMapper mapper)
        {
            _components = components;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveComponentCommand<TComp> request, CancellationToken cancellationToken)
        {


            _components.ProcessComponentReplicate(request.Item, request.State);
            await _unit.SaveChangesAsync(cancellationToken);

            return request.Item?.Id ?? Guid.Empty;
        }
    }
}
