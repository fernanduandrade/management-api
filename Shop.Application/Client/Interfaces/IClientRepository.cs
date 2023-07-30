using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Interfaces;

public interface IClientRepository
{
    Task<Entities.Client> FindByIdAsync(long id);
    void SetEntityStateModified(Entities.Client entity);
}