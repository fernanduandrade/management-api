using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;

namespace Shop.Application.Product.Queries;

public class GetQuizByIdQuery : IRequest<ApiResult<ProductDTO>>
{
    public int Id { get; init; }
}

public class GetquizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    public async Task<ApiResult<ProductDTO>> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Where(quiz => quiz.Id == request.Id)
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return new ApiResult<ProductDTO>(result, "Operação concluida com sucesso");
    }
}