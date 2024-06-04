using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;
using Manager.Domain.Products;

namespace Manager.Application.Products.GetAutoComplete;

public sealed class GetAutoCompleteQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    ILogger<GetAutoCompleteQueryHandler> logger)
    : IRequestHandler<GetAutoCompleteQuery, ApiResult<IEnumerable<ProductDto>>>
{
    public Task<ApiResult<IEnumerable<ProductDto>>> Handle(GetAutoCompleteQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Pesquisando por {SearchTerm}", request.Search);
        var autoCompleteQuery = productRepository
            .Get(x => x.Name.ToLower().Contains(request.Search.ToLower()));
        var products = autoCompleteQuery.AsEnumerable();
        
        logger.LogInformation("Quantidade de produtos encontrados {ProductQuantity}", products.Count());
        var dto = mapper.Map<IEnumerable<ProductDto>>(products);

        return Task.FromResult(new ApiResult<IEnumerable<ProductDto>>(dto, ResponseTypeEnum.Success, "Sucesso"));
    }
}