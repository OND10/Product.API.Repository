using Microsoft.Extensions.Options;
using ProductAPI.VSA.Settings;

namespace ProductAPI.VSA.Services
{
    public class TenantService : ITenantService
    {

        private HttpContext? _httpContext;
        private readonly TenantSettings _tenantSettings;
        private Tenant? _currentTenant;
        public TenantService(IHttpContextAccessor contextAccessor, IOptions<TenantSettings> tenantSettings)
        {
            _httpContext = contextAccessor.HttpContext;
            _tenantSettings = tenantSettings.Value;

            if (_httpContext is not null)
            {
                if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
                {
                    SetCurrentTenant(tenantId!);
                }

                else
                {
                    throw new Exception("No Tenant Provided!!");
                }
            }

        }
        public string? GetConnectionString()
        {
            var currentConnectionString = SetConnectionString();
            return currentConnectionString;
        }

        public Tenant? GetCurrentTenant()
        {
            return _currentTenant;
        }

        private string SetConnectionString()
        {
            var result = _currentTenant is null
                 ? _tenantSettings.Defaults.ConnectionString
                 : _currentTenant.ConnectionString;

            return result!;
        }

        public string? GetDataBaseProvider()
        {
            return _tenantSettings.Defaults.DbProvider;
        }

        private void SetCurrentTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Tenants.FirstOrDefault(t => t.TId == tenantId);


            if (_currentTenant is null)
            {
                throw new Exception("Invalid Tenant Id");
            }

            if (string.IsNullOrEmpty(_currentTenant.ConnectionString))
            {
                _currentTenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
            }
        }
    }
}
