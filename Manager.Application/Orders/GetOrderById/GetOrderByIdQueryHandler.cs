using AutoMapper;
using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;
using Manager.Domain.Orders;

namespace Manager.Application.Orders.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResult<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }
    public async Task<ApiResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindByIdAsync(request.Id);

        var dto = _mapper.Map<OrderDto>(order);

        return new ApiResult<OrderDto>(dto, ResponseTypeEnum.Success, "Sucesso");
    }
}