using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Products;

namespace Shop.Application.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        => (_unitOfWork, _productRepository) = (unitOfWork, productRepository);
    public async Task<ApiResult<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Remove(request.id);
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
    }
}