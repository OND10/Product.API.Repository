using MediatR;
using OnMapper;
using ProductAPI.Abstractions.Messaging;
using ProductAPI.VSA.Domain;
using ProductAPI.VSA.ExceptionHandler;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using ProductAPI.VSA.Features.Products.Requests.DTOs;

namespace ProductAPI.VSA.Features.Products.Requests.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand, ProductResponseDto>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitofWork _unitofWork;
        private readonly OnMapping _mapper;
        public AddProductCommandHandler(IProductRepository repository, IUnitofWork unitofWork, OnMapping mapper)
        {
            _repository = repository;
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductResponseDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var model = await _mapper.Map<AddProductCommand, Product>(request);
            var result = await _repository.CreateAsync(model.Data);
            await _unitofWork.SaveChangesAsync();
            var mappedResult = await _mapper.Map<Product, ProductResponseDto>(result);

            return await Result<ProductResponseDto>.SuccessAsync(mappedResult.Data, "Created Successfully", true);
        }
    }
}
