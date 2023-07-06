using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.Persistence;

namespace Shop.IntegrationTest.Setup;

public class ClientFixture : IClassFixture<WebApiFactoryConfig<Program, AppDbContext>>
{
    private readonly HttpClient _client;
    protected readonly SeedCreator SeedWork;
    protected readonly AppDbContext DbContext;
    private readonly  string _defaultUserID;

    public ClientFixture(WebApiFactoryConfig<Program, AppDbContext> factory)
    {
        var scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetService<AppDbContext>();
        _client = factory.CreateClient();
        SeedWork = scope.ServiceProvider.GetService<SeedCreator>();
        _defaultUserID = factory.DefaultUserId = "2321";
    }

    public async Task<HttpResponseMessage> AsGetAsync(string url)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        var result = await _client.GetAsync(url);
        return result;
    }
}