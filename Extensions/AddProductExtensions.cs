using OnMapper;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Common.Connection;
using ProductAPI.VSA.Features.Products.Repository.Implementation;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using ProductAPI.VSA.Features.Products.Requests.DTOs;
using ProductAPI.VSA.Features.Products.Requests.Queries;
using ProductAPI.VSA.Features.Videos.Queries;
using ProductAPI.VSA.Services;
using System.Reflection;

namespace ProductAPI.VSA.Extensions
{
    public static class AddProductExtensions
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUnitofWork, UnitofWork>();
            ////builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            builder.Services.AddScoped<OnMapping>();
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IJobService, JobService>();

            // These pipeline to register meditar design pattern
            //var assembly = Assembly.GetExecutingAssembly();
            //builder.Services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(assembly);
            //});

            builder.Services.AddScoped<IQueryHandler<GetProductQuery, IEnumerable<ProductResponseDto>>, GetProductQueryHandler>();
            builder.Services.AddScoped<IQueryHandler<GetGeneratedVideoQuery, List<string>>, GetGeneratedVideoQueryHandler>();

            return builder;
        }
    }
}
