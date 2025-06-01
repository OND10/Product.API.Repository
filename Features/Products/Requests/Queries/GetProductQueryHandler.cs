using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using OnMapper;
using OnMapper.Common.Exceptions;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Domain;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using ProductAPI.VSA.Features.Products.Requests.DTOs;
using System.Text.Json.Serialization;

namespace ProductAPI.VSA.Features.Products.Requests.Queries
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IProductRepository _repository;
        private readonly OnMapping _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "products";
        private readonly IHttpClientFactory _httpClient;

        public GetProductQueryHandler(IProductRepository repository,
            OnMapping mapper,
            IMemoryCache memoryCache,
            IHttpClientFactory httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _httpClient = httpClient;
        }

        public async Task<Result<IEnumerable<ProductResponseDto>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {

            if (!_memoryCache.TryGetValue(CacheKey, out Result<List<ProductResponseDto>>? products))
            {

                var result = await _repository.GetAllAsync();

                products = await _mapper.MapCollection<Product, ProductResponseDto>(result);
                
                if (products.Data.Count < 0)
                {
                    return await Result<IEnumerable<ProductResponseDto>>.FaildAsync(false, "Not viewed");
                }
                else
                {
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(2),
                        Size = 1024,
                    };
                    _memoryCache.Set(CacheKey, products, cacheExpiryOptions);


                    ////var prices = await _httpClient.GetFromJsonAsync<List<decimal>>();
                    //    var client = _httpClient.CreateClient("Pricing");
                    //    var response = await client.GetAsync("/api/Pricing");
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var apiContent = await response.Content.ReadAsStringAsync();
                    //    Console.WriteLine(apiContent);

                    //}
                    
                    return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(products.Data, "Viewed Successfully", true);
                }
            }

            return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(products.Data, "Viewed Successfully", true);
        }
    }
}
