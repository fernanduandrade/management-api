using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record UpdateSaleCommand : IRequest<ApiResult<bool>>
{
    public long Id { get; init; }
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateSaleCommandHandler(IAppDbContext context)
        => (_context) = (context);
    
    public async Task<ApiResult<bool>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.Id);

        if(entity is null)
            return new ApiResult<bool>(false, "Falha ao executar a operação");
    
        Entities.Sale updateEntity = new()
        {
            Id = request.Id,
            ClientName = request.ClientName,
            ProductName = request.ProductName,
            TotalPrice = request.TotalPrice,
            PricePerUnit = request.PricePerUnit,
            SaleDate = request.SaleDate,
            Quantity = request.Quantity
        };

        updateEntity.AddDomainEvent(new SaleCreateEvent(updateEntity));
        _context.Sales.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
} 