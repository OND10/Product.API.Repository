using MediatR;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.ExceptionHandler;
using ProductAPI.VSA.Features.Products.Requests.DTOs;

namespace ProductAPI.VSA.Features.Products.Requests.Queries
{
    public class GetProductQuery : IQuery<IEnumerable<ProductResponseDto>>
    {
    }
}
