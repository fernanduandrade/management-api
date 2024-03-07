using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.DeleteBulk;

public class DeleteOrdersBulkCommandHandler : IRequestHandler<DeleteOrdersBulkCommand, ApiResult<Unit>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IOrderRepository _orderRepository;

  public DeleteOrdersBulkCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<ApiResult<Unit>> Handle(DeleteOrdersBulkCommand request, CancellationToken cancellationToken)
  {
    _orderRepository.DeleteBulk(request.Ids);
    await _unitOfWork.Commit(cancellationToken);

    return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
  }
}
