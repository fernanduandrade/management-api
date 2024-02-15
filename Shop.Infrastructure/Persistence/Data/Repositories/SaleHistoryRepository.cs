using Microsoft.EntityFrameworkCore;
using Shop.Domain.SalesHistory;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class SaleHistoryRepository : ISaleHistoryRepository
{
    private readonly AppDbContext _context;

    public SaleHistoryRepository(AppDbContext context)
        => (_context) = (context);

    public async Task<List<SaleHistory>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var result = await _context.SalesHistory
            .AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

        return result;
    }

    public async Task<SaleHistory> FindByIdAsync(Guid id)
    {
        var saleHistory = await _context.SalesHistory.FirstOrDefaultAsync(x => x.Id == id);
        return saleHistory;
    }

    public void Add(SaleHistory saleHistory)
        => _context.SalesHistory.Add(saleHistory);

    public void AddMany(List<SaleHistory> saleHistories)
    {
        _context.SalesHistory.AddRange(saleHistories);
    }

    public void Update(SaleHistory saleHistory)
        => _context.SalesHistory.Update(saleHistory);

    public async Task Remove(Guid id)
    {
        var saleHistory = await _context.SalesHistory.FirstOrDefaultAsync(x => x.Id == id);
        _context.Remove(saleHistory);
    }

    public decimal TodaySales()
    {
        DateTime compareDate = DateTime.UtcNow;
        var todaySales = _context.SalesHistory
            .Where(x => x.Date.Day == compareDate.Day)
            .ToList()
            .Sum(x => x.TotalPrice);

        return todaySales;
    }

    public virtual void SetEntityStateModified(SaleHistory entity)
    {
        _context.SalesHistory.Entry(entity).State = EntityState.Modified;
    }
}