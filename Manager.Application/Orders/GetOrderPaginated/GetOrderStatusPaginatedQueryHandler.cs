using AutoMapper;
using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Orders.Dtos;
using Manager.Domain.Orders;

namespace Manager.Application.Orders.GetOrderPaginated;

public class GetOrderStatusPaginatedQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrderStatusPaginatedQuery, ApiResult<PaginatedList<OrderDto>>>
{
    public async Task<ApiResult<PaginatedList<OrderDto>>> Handle(GetOrderStatusPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var records = orderRepository.GetAllByStatus(request.Status);
        var result = await PaginatedList<OrderDto>
            .CreateAsync(records, request.PageNumber, request.PageSize, mapper);
        return new ApiResult<PaginatedList<OrderDto>>(result, ResponseTypeEnum.Success, "Sucesso");
    }
}