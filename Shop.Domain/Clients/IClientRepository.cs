using System.Linq.Expressions;

namespace Shop.Domain.Clients;

public interface IClientRepository
{
  void SetEntityStateModified(Client client);
  IQueryable<Client> GetAll(Expression<Func<Client, bool>>? filter = null);
  Task<Client> FindByIdAsync(Guid id);
  void Add(Client client);
  void Update(Client client);
  Task Remove(Guid id);
  void DeleteBulk(List<Guid> ids);

}