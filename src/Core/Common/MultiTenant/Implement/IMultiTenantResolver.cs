
namespace SharedApplication.MultiTenant.Implement
{
    public interface IMultiTenantResolver
    {
        Guid GetTenantId();
        bool IsSuperAdmin();
    }
}