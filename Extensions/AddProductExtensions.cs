using OnMapper;
using ProductAPI.VSA.Features.Products.Repository.Implementation;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using System.Reflection;

namespace ProductAPI.VSA.Extensions
{
    public static class AddProductExtensions
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder  builder)
        {

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            builder.Services.AddScoped<OnMapping>();
            builder.Services.AddMemoryCache();
            var assembly = Assembly.GetExecutingAssembly();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            return builder;
        }
    }
}
