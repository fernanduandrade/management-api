using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Products.Dtos;
using Shop.Domain.Products;

namespace Shop.Application.Products.GetAllProductPaginated;

public sealed class GetAllProductPaginatedQueryHandler : IRequestHandler<GetAllProductPaginatedQuery,
    ApiResult<PaginatedList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductPaginatedQueryHandler(IProductRepository productRepository, IMapper mapper)
        => (_productRepository, _mapper) = (productRepository, mapper);

    public async Task<ApiResult<PaginatedList<ProductDto>>> Handle(GetAllProductPaginatedQuery request,
        CancellationToken cancellationTokens)
    {
        var records = _productRepository.GetAllPaginated();
        
        var pagination = await PaginatedList<ProductDto>
            .CreateAsync(records, request.PageNumber, request.PageSize, _mapper);
        return new ApiResult<PaginatedList<ProductDto>>(pagination, ResponseTypeEnum.Success,
            message: "Operation completed successfully.");
    }
}