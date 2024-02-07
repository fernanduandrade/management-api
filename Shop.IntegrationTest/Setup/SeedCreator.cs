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
            new Client { Id = new Guid(), Name = "Marina", LastName = "Visgueira", Credit = 0, Debt = 12, Phone = "00000000000", IsActive = false },
            new Client { Id = new Guid(), Name = "Andre", LastName = "Osvaldo", Credit = 0, Debt = 23, Phone = "00000000000", IsActive = true },
            new Client { Id = new Guid(), Name = "Alfredo", LastName = "Siqueira", Credit = 80, Debt = 0, Phone = "00000000000", IsActive = false },
            new Client { Id = new Guid(), Name = "Clara", LastName = "Siqueira", Credit = 120, Debt = 0, Phone = "00000000000", IsActive = true }
        };

        _context.Clients.AddRange(clients);
        _context.SaveChanges();
    }
    
    public void AddProducts()
    {
        List<Product> products = new()
        {
            new Product { Id = new Guid(), Name = "Bleach", Description = "Cleaner", Price = 80, Quantity = 3 },
            new Product { Id = new Guid(), Name = "Coconout", Description = "Fruit", Price = 10, Quantity = 43 },
            new Product { Id = new Guid(), Name = "Bean", Description = "Good for lunch", Price = 23, Quantity = 0 },
            new Product { Id = new Guid(), Name = "Wine", Description = "30 years old", Price = 122, Quantity = 11 },
            new Product { Id = new Guid(), Name = "Brush Teeth", Description = "Cleaner", Price = 4, Quantity = 0 },
        };

        _context.Products.AddRange(products);
        _context.SaveChanges();
    }
    
    public void AddSales()
    {
        List<SalesHistory> sales = new()
        {
            new SalesHistory { Id = new Guid(), Quantity = 2, Date = DateTime.UtcNow, ClientId = new Guid(), TotalPrice = 20, PricePerUnit = 120, ProductId = new Guid() },
            new SalesHistory { Id = new Guid(), Quantity = 1, Date = DateTime.UtcNow, ClientId = new Guid(), TotalPrice = 40, PricePerUnit = 40, ProductId = new Guid() },
            new SalesHistory { Id = new Guid(), Quantity = 2, Date = DateTime.UtcNow, ClientId = new Guid(), TotalPrice = 4, PricePerUnit = 2, ProductId = new Guid() },
            new SalesHistory { Id = new Guid(), Quantity = 2, Date = DateTime.UtcNow, ClientId = new Guid(), TotalPrice = 8, PricePerUnit = 4, ProductId = new Guid() },
            new SalesHistory { Id = new Guid(), Quantity = 1, Date = DateTime.UtcNow, ClientId = new Guid(), TotalPrice = 120, PricePerUnit = 120, ProductId = new Guid() },
        };

        _context.Sales.AddRange(sales);
        _context.SaveChanges();
    }
}