using ProductAPI.VSA.Domain;

namespace ProductAPI.VSA.Features.Products.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product model);
       
    }
}
