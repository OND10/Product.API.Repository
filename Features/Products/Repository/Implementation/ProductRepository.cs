using Microsoft.EntityFrameworkCore;
using ProductAPI.VSA.Common.Exception;
using ProductAPI.VSA.Data;
using ProductAPI.VSA.Domain;
using ProductAPI.VSA.Features.Products.Repository.Interface;

namespace ProductAPI.VSA.Features.Products.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Product> CreateAsync(Product model)
        {
            try
            {
                var add = await _context.AddAsync(model);

                return await Task<Product>.FromResult<Product>(add.Entity);
            }
            catch
            {
                throw new ModelNullException($"{model}", "Model is null");
            }
        }

     
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var list = await _context.Products.AsNoTrackingWithIdentityResolution().ToListAsync();
            try
            {
                list = await _context.Products.ToListAsync();
                if (list.Count < 0)
                {
                    return await Task<IEnumerable<Product>>.FromResult<IEnumerable<Product>>(list);
                }
                else
                {
                    return await Task<IEnumerable<Product>>.FromResult<IEnumerable<Product>>(list);
                }
            }
            catch
            {
                return await Task<IEnumerable<Product>>.FromResult<IEnumerable<Product>>(list);
            }
        }

       
    }
}
