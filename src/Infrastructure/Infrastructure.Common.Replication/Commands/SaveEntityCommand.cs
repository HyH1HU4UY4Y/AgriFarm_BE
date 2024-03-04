using AutoMapper;
using EventBus.Defaults;
using EventBus.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Replication.Commands
{
    public class SaveEntityCommand<T> : IRequest<Guid> where T : class, IBaseEntity<Guid>
    {
        public T Item { get; set; }
        public EventState State { get; set; }
    }

    public class SaveEntityCommandHandler<TEntity, TDbContext> : IRequestHandler<SaveEntityCommand<TEntity>, Guid>
        where TEntity : class, IBaseEntity<Guid> where TDbContext : DbContext
    {

        private ISQLRepository<TDbContext, TEntity> _entities;
        private IUnitOfWork<TDbContext> _unit;
        private ILogger<SaveEntityCommandHandler<TEntity, TDbContext>> _logger;
        private IMapper _mapper;

        public SaveEntityCommandHandler(ISQLRepository<TDbContext, TEntity> entities,
            IUnitOfWork<TDbContext> unit,
            ILogger<SaveEntityCommandHandler<TEntity, TDbContext>> logger,
            IMapper mapper)
        {
            _entities = entities;
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {

            await _entities.ProcessReplicate(request.Item, request.State);
            await _unit.SaveChangesAsync(cancellationToken);

            return request.Item?.Id ?? Guid.Empty;
        }
    }
}
