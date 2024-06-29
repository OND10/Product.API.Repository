using OnMapper.Common.Exceptions;
using ProductAPI.VSA.Features.Products.Requests.DTOs;

namespace ProductAPI.VSA.Features
{
    public interface IProductController
    {
        public Task<Result<ProductResponseDto>> Post(ProductRequestDto requestDto, CancellationToken cancellationToken);

        public Task<Result<IEnumerable<ProductResponseDto>>> Get(CancellationToken cancellationToken);
        
    }
}
