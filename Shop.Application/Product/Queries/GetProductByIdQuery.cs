using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;

namespace Shop.Application.Product.Queries;

public sealed record GetProductByIdQuery : IRequest<ApiResult<ProductDTO>>
{
    public long Id { get; init; }
}

public class GetquizByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    public async Task<ApiResult<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Where(product => product.Id == request.Id)
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        
        if(result is null)
            return new ApiResult<ProductDTO>(null, ResponseTypeEnum.Warning,"Failed to find the register.");
        
        return new ApiResult<ProductDTO>(result, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}