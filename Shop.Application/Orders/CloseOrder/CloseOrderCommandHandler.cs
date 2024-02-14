using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.CloseOrder;

public sealed class CloseOrderCommandHandler : IRequestHandler<CloseOrderCommand, ApiResult<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public CloseOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }
    public async Task<ApiResult<Unit>> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.OrderId);
        
        order.CloseOrder();

        order.DispatchProductsSold(order.OrderProducts, order.ClientName, request.PaymentType);
        
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
    }
}