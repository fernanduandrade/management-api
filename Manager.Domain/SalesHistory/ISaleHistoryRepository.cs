using System.Linq.Expressions;

namespace Manager.Domain.SalesHistory;

public interface ISaleHistoryRepository
{
    IQueryable<SaleHistory> GetAllPaginated();
    Task<SaleHistory> FindByIdAsync(Guid id);
    void Add(SaleHistory saleHistory);
    void AddMany(List<SaleHistory> saleHistories);
    void Update(SaleHistory saleHistory);
    Task Remove(Guid id);
    void DeleteBulk(List<Guid> ids);
    IQueryable<SaleHistory> Get(Expression<Func<SaleHistory, bool>>? filter = null, bool readOnly = true);
}