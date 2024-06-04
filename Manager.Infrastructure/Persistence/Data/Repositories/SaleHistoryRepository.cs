using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Manager.Domain.SalesHistory;

namespace Manager.Infrastructure.Persistence.Data.Repositories;

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

    public IQueryable<SaleHistory> Get(Expression<Func<SaleHistory, bool>>? filter = null, bool readOnly = true)
    {
        return _repository.GetAll(filter, readOnly);
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
        return await  _dbSet.Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(SaleHistory saleHistory)
        => _repository.Add(saleHistory);

    public void AddMany(List<SaleHistory> saleHistories)
    {
        _dbSet.AddRange(saleHistories);
    }

    public void Update(SaleHistory saleHistory)
        => _repository.Update(saleHistory);

    public async Task Remove(Guid id)
    {
        _repository.Remove(id);
    }
    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    }
}