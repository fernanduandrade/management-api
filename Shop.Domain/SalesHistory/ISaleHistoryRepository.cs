using SharedKernel;

namespace Shop.Domain.SalesHistory;

public interface ISaleHistoryRepository : IRepository<SaleHistory>
{
    void SetEntityStateModified(SaleHistory saleHistory);
    Task<List<SaleHistory>> GetAllPaginated(int pageSize, int pageNumber);
    Task<SaleHistory> FindByIdAsync(Guid id);
    void Add(SaleHistory saleHistory);
    void AddMany(List<SaleHistory> saleHistories);
    void Update(SaleHistory saleHistory);
    Task Remove(Guid id);
}