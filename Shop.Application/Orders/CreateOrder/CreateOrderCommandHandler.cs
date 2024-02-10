using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.CreateOrder;

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