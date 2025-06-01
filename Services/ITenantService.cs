using ProductAPI.VSA.Settings;

namespace ProductAPI.VSA.Services
{
    public interface ITenantService
    {
        public string? GetDataBaseProvider();
        public string? GetConnectionString();
        public Tenant? GetCurrentTenant();
    }
}
