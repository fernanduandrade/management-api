using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record CreateSaleCommand : IRequest<ApiResult<SaleDTO>>
{
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;

    public CreateSaleCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult<SaleDTO>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        Entities.Sale entity = new()
        {	
            SaleDate = request.SaleDate,	
            ClientName = request.ClientName,	
            ProductName = request.ProductName,	
            TotalPrice = request.TotalPrice,	
            Quantity = request.Quantity,
            PricePerUnit = request.PricePerUnit
        };
        
        entity.AddDomainEvent(new SaleCreateEvent(entity));
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        SaleDTO dto = new()
        {
            Id = entity.Id,
            SaleDate = entity.SaleDate,	
            ClientName = entity.ClientName,	
            ProductName = entity.ProductName,	
            TotalPrice = entity.TotalPrice,	
            Quantity = entity.Quantity,
            PricePerUnit = entity.PricePerUnit,
        };
        
        if (entity.Id <= 0)
            return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}