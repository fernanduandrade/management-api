using System.Net;
using System.Text.Json;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;
using Shop.IntegrationTest.Setup;

namespace Shop.IntegrationTest;

public class ClienControllerTest : ClientFixture
{
    public ClienControllerTest(WebApiFactoryConfig<Program, AppDbContext> factory) : base(factory) {}

    [Fact]
    public async Task Get_Clients_Return_200()
    {
        await SeedWork.AddClients();

        var response = await AsGetAsync("/api/v1/Client");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}