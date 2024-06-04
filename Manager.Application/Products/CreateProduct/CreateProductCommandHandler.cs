using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;
using Manager.Domain.Products;

namespace Manager.Application.Products.CreateProduct;

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
        var productExistsQuery = _productRepository.Get(product => product.Name.ToUpper() == request.Name.ToUpper());

        if(productExistsQuery.Any())
            return new ApiResult<ProductDto>(new ProductDto(),ResponseTypeEnum.Warning, "Produto já cadastrado");

        var entity = Product.Create(request.Description, request.Name, request.Price, request.Quantity);
        
        _productRepository.Add(entity);
        await _unitOfWork.Commit(cancellationToken);

        ProductDto dto = _mapper.Map<ProductDto>(entity);
        
        return new ApiResult<ProductDto>(dto, ResponseTypeEnum.Success, "Sucesso"); 
    }
}