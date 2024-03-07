using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Products;

namespace Shop.Application.Products.DeleteBulk;

public class DeleteProductsBulkCommandHandler : IRequestHandler<DeleteProductsBulkCommand, ApiResult<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductsBulkCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _productRepository = productRepository;
    }

    public async Task<ApiResult<Unit>> Handle(DeleteProductsBulkCommand request, CancellationToken cancellationToken)
    {
        _productRepository.DeleteBulk(request.Ids);
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
    }
}
