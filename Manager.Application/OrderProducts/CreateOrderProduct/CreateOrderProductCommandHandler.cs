using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.OrderProducts.Dtos;
using Manager.Domain.OrderProducts;
using Manager.Domain.Orders;

namespace Manager.Application.OrderProducts.CreateOrderProduct;

public sealed class CreateOrderProductCommandHandler(
    IOrderProductRepository orderProductRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IOrderRepository orderRepository)
    : IRequestHandler<CreateOrderProductCommand, ApiResult<OrderProductDto>>
{
    public async Task<ApiResult<OrderProductDto>> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
    {
        var orderProduct = await orderProductRepository
            .Get(x => x.OrderId == request.OrderId && x.ProductId == request.ProductId)
            .FirstOrDefaultAsync();

        if (orderProduct is null)
        {
            var newOrderProduct = OrderProduct.Create(request.ProductId, request.OrderId);

            orderProductRepository.Add(newOrderProduct);
            await unitOfWork.Commit(cancellationToken);

            var dto = mapper.Map<OrderProductDto>(newOrderProduct);

            return new ApiResult<OrderProductDto>(dto, ResponseTypeEnum.Success, "Sucesso");
        }

        
        var order = await orderRepository.FindByIdAsync(orderProduct.OrderId);
        if(order.Status == OrderStatus.FECHADO)
            return new ApiResult<OrderProductDto>(null, ResponseTypeEnum.Error, "Operação não permitida");


        orderProduct.IncrementQuantity();
        await unitOfWork.Commit(cancellationToken);
        var updateDto = mapper.Map<OrderProductDto>(orderProduct);
        return new ApiResult<OrderProductDto>(updateDto, ResponseTypeEnum.Success, "Sucesso");
    }
}