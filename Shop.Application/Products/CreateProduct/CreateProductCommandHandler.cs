using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;
using Shop.Domain.Products;

namespace Shop.Application.Products.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResult<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool productExists = await _productRepository.IsProductUniqueAsync(request.Name);

        if(productExists)
            return new ApiResult<ProductDto>(new ProductDto(),ResponseTypeEnum.Warning, "Produto já cadastrado");

        var entity = Product.Create(request.Description, request.Name, request.Price, request.Quantity);
        
        _productRepository.Add(entity);
        await _unitOfWork.Commit(cancellationToken);

        ProductDto dto = _mapper.Map<ProductDto>(entity);
        
        return new ApiResult<ProductDto>(dto, ResponseTypeEnum.Success, "Sucesso"); 
    }
}