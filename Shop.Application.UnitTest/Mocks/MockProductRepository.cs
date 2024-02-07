using Shop.Application.Product.Interfaces;
using Entities = Shop.Domain.Entities;
namespace Shop.Application.UnitTest.Mocks;

public class MockProductRepository
{
    public static Mock<IProductRepository> GetProductRepository()
    {
        var products = GetProductData();

        var mockRepo = new Mock<IProductRepository>();
        mockRepo
            .Setup(x => x.GetAllPaginated(20, 1))
            .ReturnsAsync(products);

        return mockRepo;
    }

    public static List<Entities.Product> GetProductData()
    {
        List<Entities.Product> products = new()
        {
            new Entities.Product { Id = new Guid(), Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = new Guid(), Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = new Guid(), Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = new Guid(), Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = new Guid(), Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = new Guid(), Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = new Guid(), Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = new Guid(), Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = new Guid(), Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = new Guid(), Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 }
        };

        return products;
    }
}