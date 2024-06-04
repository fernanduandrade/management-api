using AutoMapper;
using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.Products.Dtos;
using Manager.Domain.Products;

namespace Manager.Application.Products.GetAllProductPaginated;

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
            message: "Operação.");
    }
}