using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Shop.Domain.SalesHistory;

namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class SaleHistoryRepository : ISaleHistoryRepository
{
    private readonly IRepository<SaleHistory> _repository;
    private readonly AppDbContext _context;
    private readonly DbSet<SaleHistory> _dbSet;

    public SaleHistoryRepository(IRepository<SaleHistory> repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
        _dbSet = _context.Set<SaleHistory>();
    }

    public IQueryable<SaleHistory> GetAll(Expression<Func<SaleHistory, bool>>? filter = null)
    {
        return _repository.GetAll(filter);
    }

    public IQueryable<SaleHistory> GetAllPaginated()
    {
        var result = _dbSet.AsNoTracking()
            .Include(x => x.Product)
            .OrderByDescending(x => x.Created);

        return result;
    }

    public async Task<SaleHistory> FindByIdAsync(Guid id)
    {
        return await _repository.FindByIdAsync(id);
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
        _repository.Remove(id);
    }

    public decimal TodaySales()
    {
        DateTime compareDate = DateTime.UtcNow;
        var todaySales = _dbSet
            .Where(x => x.Date.Date == compareDate.Date)
            .ToList()
            .Sum(x => x.TotalPrice);
        
        return todaySales;
    }

    public decimal MonthSales()
    {
        DateTime compareDate = DateTime.UtcNow;
        var todaySales = _dbSet
            .Where(x => x.Date.Month == compareDate.Month)
            .ToList()
            .Sum(x => x.TotalPrice);
        
        return todaySales;
    }

    public virtual void SetEntityStateModified(SaleHistory entity)
    {
        _repository.SetEntityStateModified(entity);
    }

    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    }
}