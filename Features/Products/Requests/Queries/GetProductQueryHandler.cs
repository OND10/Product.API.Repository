using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnMapper;
using OnMapper.Common.Exceptions;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Domain;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using ProductAPI.VSA.Features.Products.Requests.DTOs;

namespace ProductAPI.VSA.Features.Products.Requests.Queries
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, IEnumerable<ProductResponseDto>>
    {
        private readonly IProductRepository _repository;
        private readonly OnMapping _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "products";
        public GetProductQueryHandler(IProductRepository repository, OnMapping mapper, IMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
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
                    return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(products.Data, "Viewed Successfully", true);
                }
            }

            return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(products.Data, "Viewed Successfully", true);
        }
    }
}
