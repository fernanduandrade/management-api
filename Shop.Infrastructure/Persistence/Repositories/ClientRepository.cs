using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
        => (_context) = (context);
    public async Task<Client> GetByIdAsync(long id)
    {
        var entity = await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(client => client.Id == id);

        return entity;
    }
}