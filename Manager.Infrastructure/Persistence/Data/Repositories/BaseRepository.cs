using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Manager.Infrastructure.Persistence.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, bool readOnly = true)
    {
        var query = _dbSet.AsQueryable();

        if(filter is not null)
            query = query
            .Where(filter);
        
        if(readOnly)
            query.AsNoTracking();
            
        return query;
    }

    public async Task<T> FindByIdAsync(Guid id)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public void Add(T entity)
        => _dbSet.Add(entity);

    public void AddMany(List<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
    }

    public async Task Remove(Guid id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        _dbSet.Remove(entity);
    }

    public void DeleteBulk(List<Guid> ids)
    {
        var entites = _dbSet.Where(x => ids.Contains(x.Id)).ToList();
        _dbSet.RemoveRange(entites);
    }
}
