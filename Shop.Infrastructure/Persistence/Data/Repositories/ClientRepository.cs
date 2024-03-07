using Microsoft.EntityFrameworkCore;
using Shop.Domain.Clients;
namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
        => (_context) = (context);

    public IQueryable<Client> GetAllPaginated()
    {
        var result =  _context.Clients
            .AsNoTracking();

        return result;
    }

    public async Task<Client> FindByIdAsync(Guid id)
    {
        var entity = await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public void Add(Client client)
        => _context.Clients.Add(client);

    public void Update(Client client)
        => _context.Clients.Update(client);

    public async Task Remove(Guid id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        _context.Clients.Remove(client);
    }

    public virtual void SetEntityStateModified(Client entity)
    {
        _context.Clients.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteBulk(List<Guid> ids)
    {
        var clients = _context.Clients.Where(x => ids.Contains(x.Id)).ToList();
        _context.RemoveRange(clients);
    }
}