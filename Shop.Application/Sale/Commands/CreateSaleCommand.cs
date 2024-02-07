using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Application.Sale.Interfaces;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record CreateSaleCommand : IRequest<ApiResult<SaleDTO>>
{
    public DateTime SaleDate { get; set; }
    public Guid ClientId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<SaleDTO>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        Entities.SalesHistory entity = new()
        {	
            Date = request.SaleDate,	
            ClientId = request.ClientId,	
            ProductId = request.ProductId,	
            TotalPrice = request.TotalPrice,	
            Quantity = request.Quantity,
            PricePerUnit = request.PricePerUnit
        };
        
        entity.Raise(new SaleCreateEvent(entity));
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        // if (entity.Id <= 0)
        //     return new ApiResult<SaleDTO>(null, ResponseTypeEnum.Error, "Error while trying to create the register.");

        SaleDTO dto = _mapper.Map<SaleDTO>(entity);
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}