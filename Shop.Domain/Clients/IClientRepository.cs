using SharedKernel;

namespace Shop.Domain.Clients;

public interface IClientRepository : IRepository<Client>
{
    void SetEntityStateModified(Client client);
    Task<List<Client>> GetAllPaginated(int pageSize, int pageNumber);
    Task<Client> FindByIdAsync(Guid id);
    void Add(Client client);
    void Update(Client client);
    Task Remove(Guid id);
}