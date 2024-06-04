// using System.Net.Http.Headers;
// using System.Text;
// using System.Text.Json;
// using Microsoft.Extensions.DependencyInjection;
// using Manager.Infrastructure.Persistence;
//
// namespace Manager.IntegrationTest.Setup;
//
// public class ClientFixture : IClassFixture<WebApiFactoryConfig<Program, AppDbContext>>
// {
//     private readonly HttpClient _client;
//     protected readonly SeedCreator SeedWork;
//     protected readonly AppDbContext DbContext;
//     private readonly  string _defaultUserID;
//
//     public ClientFixture(WebApiFactoryConfig<Program, AppDbContext> factory)
//     {
//         var scope = factory.Services.CreateScope();
//         DbContext = scope.ServiceProvider.GetService<AppDbContext>();
//         _client = factory.CreateClient();
//         SeedWork = scope.ServiceProvider.GetService<SeedCreator>();
//         _defaultUserID = factory.DefaultUserId = "2321";
//         _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
//     }
//
//     public async Task<HttpResponseMessage> AsGetAsync(string url)
//     {
//         _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
//         var result = await _client.GetAsync(url);
//         return result;
//     }
//     
//     public async Task<HttpResponseMessage> AsPostAsync<T>(string url, T payload)
//     {
//         var json = JsonSerializer.Serialize(payload);
//         var buffer = Encoding.UTF8.GetBytes(json);
//         var bytesContent = new ByteArrayContent(buffer);
//         bytesContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//         var result = await _client.PostAsync(url, bytesContent);
//         return result;
//     }
//     
//     public async Task<HttpResponseMessage> AsPutAsync<T>(string url, T payload)
//     {
//         var json = JsonSerializer.Serialize(payload);
//         var buffer = Encoding.UTF8.GetBytes(json);
//         var bytesContent = new ByteArrayContent(buffer);
//         bytesContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//         var result = await _client.PutAsync(url, bytesContent);
//         return result;
//     }
//     
//     public async Task<HttpResponseMessage> AsDeleteAsync(string url)
//     {   
//         return await _client.DeleteAsync(url);
//     }
// }