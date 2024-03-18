using Microsoft.EntityFrameworkCore;
using Shop.Domain.Clients;
namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class ClientRepository(AppDbContext context) : IClientRepository
{
    public IQueryable<Client> GetAllPaginated()
    {
        var result =  context.Clients
            .AsNoTracking();

        return result;
    }

    public async Task<Client> FindByIdAsync(Guid id)
    {
        var entity = await context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public void Add(Client client)
        => context.Clients.Add(client);

    public void Update(Client client)
        => context.Clients.Update(client);

    public async Task Remove(Guid id)
    {
        var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        context.Clients.Remove(client);
    }

    public virtual void SetEntityStateModified(Client entity)
    {
        context.Clients.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteBulk(List<Guid> ids)
    {
        var clients = context.Clients.Where(x => ids.Contains(x.Id)).ToList();
        context.RemoveRange(clients);
    }
}