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
        var result = await _productRepository.GetAllPaginated(request.PageSize, request.PageNumber);
        var dtos = _mapper.Map<List<ProductDto>>(result);
        var paginate = new PaginatedList<ProductDto>(dtos, dtos.Count, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<ProductDto>>(paginate, ResponseTypeEnum.Success,
            message: "Operation completed successfully.");
    }
}