using SharedApplication.Persistence;
using Infrastructure.FarmRegistry.Contexts;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Registration.Repositories
{
    public interface IRegistryQueryRepo: IQueryRepository<RegistrationContext, FarmRegistration>
    {
        Task<FarmRegistration> GetById(Guid id);
    }
}
