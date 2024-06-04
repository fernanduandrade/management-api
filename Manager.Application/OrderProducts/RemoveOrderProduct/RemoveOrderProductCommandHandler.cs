using MediatR;
using Microsoft.EntityFrameworkCore;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.OrderProducts.Dtos;
using Manager.Domain.OrderProducts;
using Manager.Domain.Orders;

namespace Manager.Application.OrderProducts.RemoveOrderProduct;

public sealed class RemoveOrderProductCommandHandler(
    IOrderProductRepository orderProductRepository,
    IUnitOfWork unitOfWork,
    IOrderRepository orderRepository)
    : IRequestHandler<RemoveOrderProductCommand, ApiResult<Unit>>
{
    public async Task<ApiResult<Unit>> Handle(RemoveOrderProductCommand request, CancellationToken cancellationToken)
    {

        var orderProduct = await orderProductRepository
            .Get(x => x.OrderId == request.OrderId && x.ProductId == request.ProductId)
            .FirstOrDefaultAsync();
        
        if(orderProduct is null)
            return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Error, "Relação não existe");
        
        var order = await orderRepository.FindByIdAsync(orderProduct.OrderId);
        if(order.Status == OrderStatus.FECHADO)
            return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Error, "Operação não permitida");
            
        orderProduct.DecrementQuantity();

        if (orderProduct.Quantity == 0)
        {
            orderProductRepository.Remove(orderProduct);
        }
        await unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Sucesso");
    }
}