using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;

namespace Manager.Application.Products.GetAutoComplete;

public sealed record GetAutoCompleteQuery(string Search) : IRequest<ApiResult<IEnumerable<ProductDto>>>;