using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.SalesHistory.GetTodaySales;

public sealed record GetTodaySaleQuery() :IRequest<ApiResult<decimal>> { }