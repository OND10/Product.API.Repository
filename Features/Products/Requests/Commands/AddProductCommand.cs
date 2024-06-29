using MediatR;
using OnMapper.Common.Exceptions;
using ProductAPI.VSA.Features.Products.Requests.DTOs;

namespace ProductAPI.VSA.Features.Products.Requests.Commands
{
    public class AddProductCommand : IRequest<Result<ProductResponseDto>>
    {
        public string Name { get; set; }
        public int NumberofProduct { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
