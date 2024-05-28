using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;
using Shop.Domain.Products;

namespace Shop.Application.Products.GetAutoComplete;

public sealed class GetAutoCompleteQueryHandler : IRequestHandler<GetAutoCompleteQuery, ApiResult<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAutoCompleteQueryHandler> _logger;

    public GetAutoCompleteQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAutoCompleteQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResult<List<ProductDto>>> Handle(GetAutoCompleteQuery request, CancellationToken cancellationToken)
    {
        
        var products = await _productRepository.AutoComplete(request.Search);
        _logger.LogInformation("Quantidade de produtos encontrados {ProductQuantity}", products.Count);
        var dto = _mapper.Map<List<ProductDto>>(products);

        return new ApiResult<List<ProductDto>>(dto, ResponseTypeEnum.Success, "Sucesso");
    }
}