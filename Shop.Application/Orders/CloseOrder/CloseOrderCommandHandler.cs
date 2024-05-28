using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Orders;
using Shop.Domain.SalesHistory;

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
        if (request.PaymentType is PaymentType.CANCELAR)
        {
            await _orderRepository.Remove(request.OrderId);
            await _unitOfWork.Commit(cancellationToken);
            
            return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
        }
        order.CloseOrder();

        order.DispatchProductsSold(order.OrderProducts, order.ClientName, request.PaymentType);
        
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
    }
}