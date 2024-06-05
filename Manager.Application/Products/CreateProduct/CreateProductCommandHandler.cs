using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;
using Manager.Domain.Products;
using Microsoft.Extensions.Logging;

namespace Manager.Application.Products.CreateProduct;

public sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
    : IRequestHandler<CreateProductCommand, ApiResult<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResult<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productExistsQuery = _productRepository.Get(product => product.Name.ToUpper() == request.Name.ToUpper());

        if(productExistsQuery.Any())
            return new ApiResult<ProductDto>(new ProductDto(),ResponseTypeEnum.Warning, "Produto já cadastrado");

        var entity = Product.Create(request.Description, request.Name, request.Price, request.Quantity);
        
        _productRepository.Add(entity);
        await _unitOfWork.Commit(cancellationToken);

        ProductDto dto = _mapper.Map<ProductDto>(entity);
        
        logger.LogInformation("Novo produto adicionado {ProductName}", dto.Name);
        return new ApiResult<ProductDto>(dto, ResponseTypeEnum.Success, "Sucesso"); 
    }
}