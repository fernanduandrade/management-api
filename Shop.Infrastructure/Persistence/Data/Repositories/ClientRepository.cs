using Microsoft.EntityFrameworkCore;
using Shop.Domain.Clients;
namespace Shop.Infrastructure.Persistence.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
        => (_context) = (context);

    public async Task<List<Client>> GetAllPaginated(int pageSize, int pageNumber)
    {
        var result = await _context.Clients
            .AsNoTracking()
            .Take(pageSize)
            .Skip(pageNumber)
            .ToListAsync();

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
}