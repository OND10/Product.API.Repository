namespace ProductAPI.VSA.Settings
{
    public class Tenant
    {
        public string TId { get; set; }

        public string Name { get; set; } = null!;

        // This field is an extra field unless you won't use the
        // default (shared) connection string for this tenant
        public string? ConnectionString { get; set; }

    }
}
