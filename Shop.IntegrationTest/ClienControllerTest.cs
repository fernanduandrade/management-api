using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Models;
using Shop.Infrastructure.Persistence;
using Shop.IntegrationTest.Setup;
using Xunit.Priority;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Shop.IntegrationTest;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class ClienControllerTest : ClientFixture
{
    public ClienControllerTest(WebApiFactoryConfig<Program, AppDbContext> factory) : base(factory) {}

    [Fact, Priority(-1)]
    public async Task Get_Clients_Should_Return_200()
    {
        SeedWork.AddClients();
        var response = await AsGetAsync("/api/v1/Client?pageSize=5");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_Client_Should_Return_201()
    {
        ClientDTO client = new()
        {
            Name = "Juliana", LastName = "Alves", IsActive = true, Debt = 0, Phone = "860000000", Credit = 1
        };

        var response = await AsPostAsync("/api/v1/Client", client);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Client_Should_Return_200()
    {
        var response = await AsDeleteAsync("/api/v1/Client/2");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_NonExistClient_Should_Return_400()
    {
        var response = await AsDeleteAsync("/api/v1/Client/99");
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Update_Client_Should_Return_400()
    {
        ClientDTO newClient = new(){ Id = 5, Name = "Clara", LastName = "Siqueira", Credit = 120, Debt = 0, Phone = "00000000000", IsActive = false };
        var response = await AsPutAsync("/api/v1/Client", newClient);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}