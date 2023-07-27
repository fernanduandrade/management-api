using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record UpdateSaleCommand : IRequest<ApiResult<SaleDTO>>
{
    public long Id { get; init; }
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
    public long ProductId { get; init; }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;

    public UpdateSaleCommandHandler(IAppDbContext context)
        => (_context) = (context);
    
    public async Task<ApiResult<SaleDTO>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(sale => sale.Id == request.Id);

        if(entity is null)
            return new ApiResult<SaleDTO>(new SaleDTO(), ResponseTypeEnum.Warning, "Failed to update the register.");

        Entities.Sale updateEntity = new()
        {
            Id = entity.Id,
            SaleDate = entity.SaleDate,	
            ClientName = entity.ClientName,	
            ProductFk = entity.ProductFk,	
            TotalPrice = entity.TotalPrice,	
            Quantity = entity.Quantity,
            PricePerUnit = entity.PricePerUnit,
        };
        
        updateEntity.AddDomainEvent(new SaleCreateEvent(updateEntity));
        _context.Sales.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        SaleDTO dto = new()
        {
            Id = entity.Id,
            SaleDate = entity.SaleDate,	
            ClientName = entity.ClientName,	
            ProductId = entity.ProductFk,	
            TotalPrice = entity.TotalPrice,	
            Quantity = entity.Quantity,
            PricePerUnit = entity.PricePerUnit,
        };
        
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
} 