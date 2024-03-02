using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.OrderProducts.Dtos;
using Shop.Domain.OrderProducts;
using Shop.Domain.Orders;

namespace Shop.Application.OrderProducts.RemoveOrderProduct;

public sealed class RemoveOrderProductCommandHandler
    : IRequestHandler<RemoveOrderProductCommand, ApiResult<Unit>>
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderProductCommandHandler(IOrderProductRepository orderProductRepository,
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository)
    {
        _orderProductRepository = orderProductRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }
    public async Task<ApiResult<Unit>> Handle(RemoveOrderProductCommand request, CancellationToken cancellationToken)
    {
        
        var orderProduct = await _orderProductRepository.OrderProductExist(request.ProductId, request.OrderId);
        if(orderProduct is null)
            return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Error, "Relação não existe");
        
        var order = await _orderRepository.FindByIdAsync(orderProduct.OrderId);
        if(order.Status == OrderStatus.FECHADO)
            return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Error, "Operação não permitida");
            
        orderProduct.DecrementQuantity();

        if (orderProduct.Quantity == 0)
        {
            _orderProductRepository.Remove(orderProduct);
        }
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
    }
}