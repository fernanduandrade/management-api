using AutoMapper;
using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;
using Manager.Domain.Orders;

namespace Manager.Application.Orders.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResult<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(OrderStatus.ABERTO, request.ClientName);
        
        _orderRepository.Add(order);
        await _unitOfWork.Commit(cancellationToken);

        var dto = _mapper.Map<OrderDto>(order);

        return new ApiResult<OrderDto>(dto, ResponseTypeEnum.Success, "Sucesso");
    }
}