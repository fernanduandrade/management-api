using System.Linq.Expressions;

namespace SharedKernel;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, bool readOnly = true);
    Task<T> FindByIdAsync(Guid id);
    void Add(T entity);
    void Update(T entity);
    Task Remove(Guid id);
    void DeleteBulk(List<Guid> ids);
}