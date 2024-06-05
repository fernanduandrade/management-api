using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;
using Manager.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace Manager.Application.Orders.CreateOrder;

public sealed class CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
    : IRequestHandler<CreateOrderCommand, ApiResult<OrderDto>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResult<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(OrderStatus.ABERTO, request.ClientName);
        
        _orderRepository.Add(order);
        await _unitOfWork.Commit(cancellationToken);

        var dto = _mapper.Map<OrderDto>(order);
        
        logger.LogInformation("Novo pedido adicionado para: {ClientName}", dto.ClientName);
        return new ApiResult<OrderDto>(dto, ResponseTypeEnum.Success, "Sucesso");
    }
}