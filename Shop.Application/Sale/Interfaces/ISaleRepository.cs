using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Interfaces;

public interface ISaleRepository
{
    Task<Entities.SalesHistory> FindByIdAsync(Guid id);
    void SetEntityStateModified(Entities.SalesHistory entity);
}