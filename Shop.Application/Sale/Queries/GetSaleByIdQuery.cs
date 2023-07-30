using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Application.Sale.Interfaces;

namespace Shop.Application.Sale.Queries;

public sealed record GetSaleByIdQuery : IRequest<ApiResult<SaleDTO>>
{
    public long Id { get; init; }
}

public class GetSaleByQueryHandler : IRequestHandler<GetSaleByIdQuery, ApiResult<SaleDTO>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleByQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        => (_saleRepository, _mapper) = (saleRepository, mapper);
    
    public async Task<ApiResult<SaleDTO>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _saleRepository.FindByIdAsync(request.Id);

        if(result is null)
            return new ApiResult<SaleDTO>(null, ResponseTypeEnum.Warning,"Failed to find the register.");

        var dto = _mapper.Map<SaleDTO>(result);
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}