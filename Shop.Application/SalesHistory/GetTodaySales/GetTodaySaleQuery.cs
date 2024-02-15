using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.SalesHistory.GetTodaySales;

public sealed record GetTodaySaleQuery() :IRequest<ApiResult<decimal>> { }