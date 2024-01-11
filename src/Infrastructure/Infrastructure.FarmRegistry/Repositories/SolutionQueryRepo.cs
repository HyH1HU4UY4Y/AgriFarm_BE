using SharedApplication.Persistence;
using Infrastructure.FarmRegistry.Contexts;
using SharedDomain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Registration.Repositories
{
    public class SolutionQueryRepo : QueryRepository<RegistrationContext, PackageSolution>, ISolutionQueryRepo
    {
        public SolutionQueryRepo(RegistrationContext context) : base(context)
        {
        }

        public Task<PackageSolution?> GetByName(string name)
        {
            return Task.FromResult(All.FirstOrDefault(e=>e.Name == name));
        }
    }
}
