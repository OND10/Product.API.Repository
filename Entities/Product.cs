using System.ComponentModel.DataAnnotations;

namespace ProductAPI.VSA.Domain
{
    public class Product : IMustHaveTenant
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberofProduct { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public string TenantId { get; set; } = null!;
    }
}
