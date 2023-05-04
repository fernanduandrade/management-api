using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;

namespace Shop.Application.Product.Queries;

public record GetAllProductPaginatedQuery :IRequest<ApiResult<PaginatedList<ProductDTO>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class
    GetAllProductPaginatedQueryHandler : IRequestHandler<GetAllProductPaginatedQuery,
        ApiResult<PaginatedList<ProductDTO>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductPaginatedQueryHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<PaginatedList<ProductDTO>>> Handle(GetAllProductPaginatedQuery request, CancellationToken cancellationTokens)
    {
        var result = await _context.Products
            .AsNoTracking()
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<ProductDTO>>(result, message: "Operação realizada com sucesso");
    }
}