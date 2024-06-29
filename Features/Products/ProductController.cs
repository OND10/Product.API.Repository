using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMapper;
using OnMapper.Common.Exceptions;
using ProductAPI.VSA.Features.Products.Requests.Commands;
using ProductAPI.VSA.Features.Products.Requests.DTOs;
using ProductAPI.VSA.Features.Products.Requests.Queries;

namespace ProductAPI.VSA.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly ISender _sender;
        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProductResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetProductQuery();

            var result = await _sender.Send(query, cancellationToken);
            if (result.IsSuccess)
            {
                return await Result<IEnumerable<ProductResponseDto>>.SuccessAsync(result.Data, "Viewed Successfully", true);
            }

            return await Result<IEnumerable<ProductResponseDto>>.FaildAsync(false, "Viewed Successfully");
        }

        [HttpPost]
        public async Task<Result<ProductResponseDto>>Post(ProductRequestDto request, CancellationToken cancellationToken)
        {
            var mapper = new OnMapping();
            var mappedCommand = await mapper.Map<ProductRequestDto, AddProductCommand>(request);
            
            var result = await _sender.Send(mappedCommand.Data, cancellationToken);


            return await Result<ProductResponseDto>.SuccessAsync(result.Data, "Created Successfully", true);
        }

        

    }
}
