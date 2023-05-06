using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;

namespace Shop.Application.Sale.Queries;

public sealed record GetAllSalePaginatedQuery : IRequest<ApiResult<PaginatedList<SaleDTO>>>
{
    public int PageSize { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
}

public class
    GetAllSalePaginatedQueryHandler : IRequestHandler<GetAllSalePaginatedQuery, ApiResult<PaginatedList<SaleDTO>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllSalePaginatedQueryHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    
    public async Task<ApiResult<PaginatedList<SaleDTO>>> Handle(GetAllSalePaginatedQuery request, CancellationToken cancellationToken)
    {
        var sales = await _context.Sales.AsNoTracking()
            .ProjectTo<SaleDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<SaleDTO>>(sales, "Operação concluida com sucesso.");
    }
}