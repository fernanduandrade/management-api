using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;
using Shop.Domain.Products;

namespace Shop.Application.Products.GetAutoComplete;

public sealed class GetAutoCompleteQueryHandler : IRequestHandler<GetAutoCompleteQuery, ApiResult<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAutoCompleteQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<List<ProductDto>>> Handle(GetAutoCompleteQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.AutoComplete(request.Search);

        var dto = _mapper.Map<List<ProductDto>>(products);

        return new ApiResult<List<ProductDto>>(dto, ResponseTypeEnum.Success, "Sucesso");
    }
}