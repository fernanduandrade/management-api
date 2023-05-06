using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;

namespace Shop.Application.Sale.Queries;

public sealed record GetSaleByIdQuery : IRequest<ApiResult<SaleDTO>>
{
    public long Id { get; init; }
}

public class GetSaleByQueryHandler : IRequestHandler<GetSaleByIdQuery, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetSaleByQueryHandler(IAppDbContext _context, IMapper mapper)
        => (_context, _mapper) = (_context, mapper);
    
    public async Task<ApiResult<SaleDTO>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Sales
            .AsNoTracking()
            .ProjectTo<SaleDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(entity => entity.Id == request.Id);

        if(result is null)
            return new ApiResult<SaleDTO>(null, ResponseTypeEnum.Warning,"Failed to find the register.");
        
        return new ApiResult<SaleDTO>(result, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}