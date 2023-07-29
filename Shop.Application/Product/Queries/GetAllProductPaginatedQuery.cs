using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Interfaces;

namespace Shop.Application.Product.Queries;

public sealed record GetAllProductPaginatedQuery : IRequest<ApiResult<PaginatedList<ProductDTO>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class
    GetAllProductPaginatedQueryHandler : IRequestHandler<GetAllProductPaginatedQuery,
        ApiResult<PaginatedList<ProductDTO>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductPaginatedQueryHandler(IProductRepository productRepository, IMapper mapper)
        => (_productRepository, _mapper) = (productRepository, mapper);
    public async Task<ApiResult<PaginatedList<ProductDTO>>> Handle(GetAllProductPaginatedQuery request, CancellationToken cancellationTokens)
    {
        var result = await _productRepository.GetAllPaginated(request.PageSize, request.PageNumber);
        var dtos = _mapper.Map<List<ProductDTO>>(result);
        var paginate = new PaginatedList<ProductDTO>(dtos, dtos.Count, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<ProductDTO>>(paginate, ResponseTypeEnum.Success ,message: "Operation completed successfully.");
    }
}