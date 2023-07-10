using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.IntegrationTest.Setup;

public class SeedCreator
{
    private readonly AppDbContext _context;

    public SeedCreator(AppDbContext context) => (_context) = (context);

    public void AddClients()
    {
        List<Client> clients = new()
        {
            new Client { Id = 2, Name = "Marina", LastName = "Visgueira", Credit = 0, Debt = 12, Phone = "00000000000", IsActive = false },
            new Client { Id = 3, Name = "Andre", LastName = "Osvaldo", Credit = 0, Debt = 23, Phone = "00000000000", IsActive = true },
            new Client { Id = 4, Name = "Alfredo", LastName = "Siqueira", Credit = 80, Debt = 0, Phone = "00000000000", IsActive = false },
            new Client { Id = 5, Name = "Clara", LastName = "Siqueira", Credit = 120, Debt = 0, Phone = "00000000000", IsActive = true }
        };

        _context.Clients.AddRange(clients);
        _context.SaveChanges();
    }
}