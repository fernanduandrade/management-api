using Microsoft.EntityFrameworkCore;
using Shop.Application.Sale.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
        => (_context) = (context);

    public async Task<SalesHistory> FindByIdAsync(Guid id)
    {
        var entity = await _context.Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }
    
    public virtual void SetEntityStateModified(SalesHistory entity)
    {
        _context.Sales.Entry(entity).State = EntityState.Modified;
    }
}