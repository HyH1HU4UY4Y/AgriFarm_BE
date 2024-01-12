using Infrastructure.FarmRegistry.Contexts;
using SharedApplication.Persistence;
using SharedDomain.Entities.Subscribe;
using SharedDomain.Repositories.Base;

namespace Infrastructure.Registration.Repositories
{
    public class RegistryQueryRepo : QueryRepository<RegistrationContext, FarmRegistration>, IRegistryQueryRepo
    {
        public RegistryQueryRepo(RegistrationContext context) : base(context)
        {
        }


        public Task<FarmRegistration> GetById(Guid id)
        {
            return Task.FromResult(All.FirstOrDefault(e => e.Id == id));
        }
    }
}
