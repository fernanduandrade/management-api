using System.Net;
using Shop.Application.Product.DTOs;
using Shop.Infrastructure.Persistence;
using Shop.IntegrationTest.Setup;
using Xunit.Priority;

namespace Shop.IntegrationTest;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class ProductControllerTest : ClientFixture
{
    public ProductControllerTest(WebApiFactoryConfig<Program, AppDbContext> factory) : base(factory) {}

    [Fact, Priority(-1)]
    public async Task Get_Products_Should_Return_200()
    {
        SeedWork.AddProducts();
        var response = await AsGetAsync("/api/v1/products?pageSize=5");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_Product_Should_Return_201()
    {
        var product = new ProductDTO()
            { Name = "Magic Cube", Description = "Toy", IsAvaliable = true, Price = 29, Quantity = 7 };

        var response = await AsPostAsync("/api/v1/products", product);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Product_Should_Return_200()
    {
        var response = await AsDeleteAsync("/api/v1/products/2");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_NonExistProduct_Should_Return_400()
    {
        var response = await AsDeleteAsync("/api/v1/products/99");
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Update_Product_Should_Return_200()
    {
        var product = new ProductDTO()
            { Id = 6, Name = "Magic  Cube", Description = "Magic  Cube", IsAvaliable = true,Price = 29, Quantity = 7 };
        var response = await AsPutAsync("/api/v1/products", product);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}