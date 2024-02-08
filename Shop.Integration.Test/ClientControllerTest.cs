// using System.Net;
// using Shop.Application.Client.DTOs;
// using Shop.Infrastructure.Persistence;
// using Shop.IntegrationTest.Setup;
// using Xunit.Priority;
//
// namespace Shop.IntegrationTest;
//
// [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
// public class ClientControllerTest : ClientFixture
// {
//     public ClientControllerTest(WebApiFactoryConfig<Program, AppDbContext> factory) : base(factory) {}
//
//     [Fact, Priority(-1)]
//     public async Task Get_Clients_Should_Return_200()
//     {
//         SeedWork.AddClients();
//         var response = await AsGetAsync("/api/v1/clients?pageSize=5");
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }
//     
//     [Fact]
//     public async Task Create_Client_Should_Return_201()
//     {
//         ClientDTO client = new()
//         {
//             Name = "Juliana", LastName = "Alves", IsActive = true, Debt = 0, Phone = "860000000", Credit = 1
//         };
//
//         var response = await AsPostAsync("/api/v1/clients", client);
//         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
//     }
//
//     [Fact]
//     public async Task Delete_Client_Should_Return_200()
//     {
//         var response = await AsDeleteAsync("/api/v1/clients/2");
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }
//     
//     [Fact]
//     public async Task Delete_NonExistClient_Should_Return_400()
//     {
//         var response = await AsDeleteAsync("/api/v1/clients/99");
//         
//         Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//     }
//     
//     [Fact]
//     public async Task Update_Client_Should_Return_200()
//     {
//         ClientDTO newClient = new(){ Id = 5, Name = "Clara", LastName = "Siqueira", Credit = 120, Debt = 0, Phone = "00000000000", IsActive = false };
//         var response = await AsPutAsync("/api/v1/clients", newClient);
//
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }
// }