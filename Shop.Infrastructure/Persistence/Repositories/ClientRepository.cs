using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
        => (_context) = (context);

    public async Task<Client> FindByIdAsync(Guid id)
    {
        var entity = await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }
    
    public virtual void SetEntityStateModified(Client entity)
    {
        _context.Clients.Entry(entity).State = EntityState.Modified;
    }
}