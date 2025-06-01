using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OnMapper;
using OnMapper.Common.Exceptions;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Features.Products.Requests.Commands;
using ProductAPI.VSA.Features.Products.Requests.DTOs;
using ProductAPI.VSA.Features.Products.Requests.Queries;
using ProductAPI.VSA.Features.Products.Requests.Queries.GitHub;

namespace ProductAPI.VSA.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryHandler<GetProductQuery, IEnumerable<ProductResponseDto>> _handler;
        public ProductController(
            IQueryHandler<GetProductQuery, IEnumerable<ProductResponseDto>> handler)
        {
            _handler = handler;
        }



        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var query = new GetProductQuery();
            var result = await _handler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                // Just return Ok(data) → let OData handle filtering/selecting
                return Ok(result.Data);
            }

            return NotFound(); // or BadRequest()
        }

        //[HttpPost]
        //public async Task<Result<ProductResponseDto>> Post(ProductRequestDto request, CancellationToken cancellationToken)
        //{
        //    var mapper = new OnMapping();
        //    var mappedCommand = await mapper.Map<ProductRequestDto, AddProductCommand>(request);

        //    var result = await _sender.Send(mappedCommand.Data, cancellationToken);


        //    return await Result<ProductResponseDto>.SuccessAsync(result.Data, "Created Successfully", true);
        //}

        //[HttpGet("{username}")]
        //public async Task<Result<bool>> CheckGibHub(string username, CancellationToken cancellationToken)
        //{
        //    var query = new IsValidGitHubUserQuery();
        //    query.username = username;

        //    var result = await _sender.Send(query, cancellationToken);
        //    if (result.IsSuccess)
        //    {
        //        return await Result<bool>.SuccessAsync(result.Data, "Viewed Successfully", true);
        //    }

        //    return await Result<bool>.FaildAsync(false, "Viewed Successfully");
        //}



    }
}
