using System.ComponentModel.DataAnnotations;

namespace ProductAPI.VSA.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberofProduct {  get; set; }
        public string Category {  get; set; }
        public decimal Price {  get; set; }
    }
}
