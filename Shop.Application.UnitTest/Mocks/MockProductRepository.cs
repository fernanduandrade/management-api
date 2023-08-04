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
            new Entities.Product { Id = 3, Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = 4, Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = 5, Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = 6, Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = 7, Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = 8, Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = 9, Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = 10, Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = 11, Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = 12, Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = 13, Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = 14, Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 },
            new Entities.Product { Id = 16, Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 },
            new Entities.Product { Id = 17, Name = "Bean", Description = "Bean", Price = 23, Quantity = 0 },
            new Entities.Product { Id = 18, Name = "Wine", Description = "Wine 30 years old", Price = 122, Quantity = 11 },
            new Entities.Product { Id = 19, Name = "Brush Teeth", Description = "Brush Teeth", Price = 4, Quantity = 0 }
        };

        return products;
    }
}