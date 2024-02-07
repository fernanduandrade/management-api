using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Interfaces;

namespace Shop.Application.Product.Queries;

public sealed record GetProductByIdQuery : IRequest<ApiResult<ProductDTO>>
{
    public Guid Id { get; init; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResult<ProductDTO>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        => (_productRepository, _mapper) = (productRepository, mapper);
    public async Task<ApiResult<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.FindByIdAsync(request.Id);
        if(result is null)
            return new ApiResult<ProductDTO>(null, ResponseTypeEnum.Warning,"Failed to find the record.");
        
        var dto = _mapper.Map<ProductDTO>(result);
        
        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}