using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Interfaces;

public interface ISaleRepository
{
    Task<Entities.Sale> FindByIdAsync(long id);
    void SetEntityStateModified(Entities.Sale entity);
}