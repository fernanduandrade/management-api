using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;

namespace Shop.Application.SalesHistory.GetSaleHistoryById;

public sealed record GetSaleHistoryByIdQuery(Guid Id) : IRequest<ApiResult<SaleHistoryDto>>;