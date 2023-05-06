using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Client.Queries;

public sealed record GetClientByIdQuery : IRequest<ApiResult<ClientDTO>>
{
    public long Id { get; init; }
}

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery ,ApiResult<ClientDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetClientByIdQueryHandler(IAppDbContext context, IMapper mapper)
        => (_context) = (context);
    public async Task<ApiResult<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Where(product => product.Id == request.Id)
            .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if(result is null)
            return new ApiResult<ClientDTO>(null, ResponseTypeEnum.Warning,"Failed to find the register.");
        
        return new ApiResult<ClientDTO>(result, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}