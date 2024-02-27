using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using Infrastructure.Supply.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Supply.Commands.Base
{
    public class SaveComponentCommand<T> : IRequest<Guid> where T : BaseComponent
    {
        public T Item { get; set; }
        public EventState State { get; set; }
    }

    public class SaveComponentCommandHandler<TComp> : IRequestHandler<SaveComponentCommand<TComp>, Guid> where TComp : BaseComponent
    {

        private ISQLRepository<SupplyContext, BaseComponent> _components;
        private IUnitOfWork<SupplyContext> _unit;
        private ILogger<SaveComponentCommandHandler<TComp>> _logger;
        private IMapper _mapper;

        public SaveComponentCommandHandler(ISQLRepository<SupplyContext, BaseComponent> components,
            IUnitOfWork<SupplyContext> unit,
            ILogger<SaveComponentCommandHandler<TComp>> logger,
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

            return request.Item?.Id??Guid.Empty;
        }
    }
}
