using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.IntegrationTest.Setup;

public class SeedCreator
{
    private readonly AppDbContext _context;

    public SeedCreator(AppDbContext context) => (_context) = (context);

    public async Task AddClients()
    {
        List<Client> clients = new()
        {
            new Client { Name = "Joaquim", LastName = "Andrade", Credit = 80, Phone = "00000000000", IsActive = true },
            new Client { Name = "Marina", LastName = "Visgueira", Credit = 10, Phone = "00000000000", IsActive = false },
            new Client { Name = "Andre", LastName = "Osvaldo", Credit = 0, Debt = 23, Phone = "00000000000", IsActive = true },
            new Client { Name = "Alfredo", LastName = "Siqueira", Credit = 80, Phone = "00000000000", IsActive = false },
            new Client { Name = "Clara", LastName = "Siqueira", Credit = 120, Phone = "00000000000", IsActive = true }
        };

        await _context.AddRangeAsync(clients);
        await _context.SaveChangesAsync();
    }
}