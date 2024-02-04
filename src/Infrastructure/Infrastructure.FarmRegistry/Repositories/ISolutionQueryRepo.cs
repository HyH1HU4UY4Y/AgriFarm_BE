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
    public interface ISolutionQueryRepo: IQueryRepository<RegistrationContext, PackageSolution>
    {

        
        Task<PackageSolution?> GetByName(string name);

    }
}
