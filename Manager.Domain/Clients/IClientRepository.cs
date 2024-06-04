using System.Linq.Expressions;

namespace Manager.Domain.Clients;

public interface IClientRepository
{
  IQueryable<Client> Get(Expression<Func<Client, bool>>? filter = null, bool readOnly = true);
  Task<Client> FindByIdAsync(Guid id);
  void Add(Client client);
  void Update(Client client);
  Task Remove(Guid id);
  void DeleteBulk(List<Guid> ids);

}