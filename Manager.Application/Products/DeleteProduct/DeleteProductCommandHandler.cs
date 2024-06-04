using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.Products;

namespace Manager.Application.Products.DeleteProduct;

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