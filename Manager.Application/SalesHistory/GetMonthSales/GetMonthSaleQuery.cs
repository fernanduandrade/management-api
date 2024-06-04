using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.SalesHistory.GetMonthSales;

public sealed record GetMonthSaleQuery() :IRequest<ApiResult<decimal>> { }