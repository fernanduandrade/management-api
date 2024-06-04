using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;
using Shop.Domain.Products;


namespace Shop.Application.Products.UpdateProduct;


public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
        => (_productRepository, _mapper, _unitOfWork) = (productRepository, mapper, unitOfWork);

    public async Task<ApiResult<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.FindByIdAsync(request.Id);
        entity.Update(request.Name, request.Description, request.Quantity, request.Price);
        _productRepository.Update(entity);
        await _unitOfWork.Commit(cancellationToken);
        
        ProductDto dto = _mapper.Map<ProductDto>(entity);

        return new ApiResult<ProductDto>(dto, ResponseTypeEnum.Success, "Sucesso");
        
    }
}