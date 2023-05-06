using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;

namespace Shop.Application.Client.Queries;

public sealed record GetAllClientPaginatedQuery : IRequest<ApiResult<PaginatedList<ClientDTO>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1;
}

public class
    GetAllClientPaginatedQueryHandler : IRequestHandler<GetAllClientPaginatedQuery, ApiResult<PaginatedList<ClientDTO>>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllClientPaginatedQueryHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<PaginatedList<ClientDTO>>> Handle(GetAllClientPaginatedQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AsNoTracking()
            .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<ClientDTO>>(result, ResponseTypeEnum.Success ,message: "Operation completed successfully.");
    }
}