using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.SalesHistory.GetMonthSales;

public sealed record GetMonthSaleQuery() :IRequest<ApiResult<decimal>> { }