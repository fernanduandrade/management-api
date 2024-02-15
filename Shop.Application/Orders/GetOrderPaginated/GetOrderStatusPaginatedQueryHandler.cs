using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Orders;

namespace Shop.Application.Orders.GetOrderPaginated;

public class GetOrderStatusPaginatedQueryHandler
    : IRequestHandler<GetOrderStatusPaginatedQuery, ApiResult<PaginatedList<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderStatusPaginatedQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<PaginatedList<OrderDto>>> Handle(GetOrderStatusPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var orders = _orderRepository.GetAllByStatus(request.Status);
        var pagination = await PaginatedList<Order>
            .CreateAsync(orders, request.PageNumber, request.PageSize);
        var dto = _mapper.Map<List<OrderDto>>(pagination.Items);
        var result = new PaginatedList<OrderDto>(dto, pagination.TotalCount, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<OrderDto>>(result, ResponseTypeEnum.Success, "Sucesso");
    }
}