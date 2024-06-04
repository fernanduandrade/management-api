using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;

namespace Manager.Application.SalesHistory.GetSaleHistoryById;

public sealed record GetSaleHistoryByIdQuery(Guid Id) : IRequest<ApiResult<SaleHistoryDto>>;