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
    
    public void AddProducts()
    {
        List<Product> products = new()
        {
            new Product { Id = 2, Name = "Bleach", Description = "Cleaner", Price = 80, Quantity = 3, IsAvaliable = true },
            new Product { Id = 3, Name = "Coconout", Description = "Fruit", Price = 10, Quantity = 43, IsAvaliable = true },
            new Product { Id = 4, Name = "Bean", Description = "Good for lunch", Price = 23, Quantity = 0, IsAvaliable = false },
            new Product { Id = 5, Name = "Wine", Description = "30 years old", Price = 122, Quantity = 11, IsAvaliable = true },
            new Product { Id = 6, Name = "Brush Teeth", Description = "Cleaner", Price = 4, Quantity = 0, IsAvaliable = false },
        };

        _context.Products.AddRange(products);
        _context.SaveChanges();
    }
    
    public void AddSales()
    {
        List<Sale> sales = new()
        {
            new Sale { Id = 2, Quantity = 2, SaleDate = DateTime.UtcNow, ClientName = "José", TotalPrice = 20, PricePerUnit = 120, ProductFk = 2 },
            new Sale { Id = 3, Quantity = 1, SaleDate = DateTime.UtcNow, ClientName = "Maria", TotalPrice = 40, PricePerUnit = 40, ProductFk = 2 },
            new Sale { Id = 4, Quantity = 2, SaleDate = DateTime.UtcNow, ClientName = "João", TotalPrice = 4, PricePerUnit = 2, ProductFk = 3 },
            new Sale { Id = 5, Quantity = 2, SaleDate = DateTime.UtcNow, ClientName = "Rodrigo", TotalPrice = 8, PricePerUnit = 4, ProductFk = 4 },
            new Sale { Id = 6, Quantity = 1, SaleDate = DateTime.UtcNow, ClientName = "Ana", TotalPrice = 120, PricePerUnit = 120, ProductFk = 6 },
        };

        _context.Sales.AddRange(sales);
        _context.SaveChanges();
    }
}