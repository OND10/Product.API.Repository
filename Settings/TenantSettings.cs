namespace ProductAPI.VSA.Settings
{
    public class TenantSettings
    {
        public Configuration Defaults { get; set; } = default;
        public List<Tenant> Tenants { get; set; } = new();
    }
}
