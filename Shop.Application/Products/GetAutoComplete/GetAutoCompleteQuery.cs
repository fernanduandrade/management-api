using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.GetAutoComplete;

public sealed record GetAutoCompleteQuery(string Search) : IRequest<ApiResult<List<ProductDto>>>;