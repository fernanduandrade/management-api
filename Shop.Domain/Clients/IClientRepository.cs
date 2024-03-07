using SharedKernel;

namespace Shop.Domain.Clients;

public interface IClientRepository : IRepository<Client>
{
    void SetEntityStateModified(Client client);
    IQueryable<Client> GetAllPaginated();
    Task<Client> FindByIdAsync(Guid id);
    void Add(Client client);
    void Update(Client client);
    Task Remove(Guid id);
    void DeleteBulk(List<Guid> ids);
}