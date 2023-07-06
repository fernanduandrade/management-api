using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.IntegrationTest.Setup;

public class WebApiFactoryConfig<TProgram, TDbContext>
    : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram :
        class where TDbContext : DbContext
{
    public string DefaultUserId { get; set; } = "1";
    private readonly TestcontainerDatabase _container =
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "shop",
                Password = "postgres",
                Username = "postgres"
            })
            .WithImage("postgres:11")
            .WithCleanUp(true)
            .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Configure<TestAuthHandlerOptions>(options => options.DefaultUserId = DefaultUserId);
            services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
            var descriptor = services.SingleOrDefault( d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null) services.Remove(descriptor);
            services.AddDbContext<TDbContext>(options => { options.UseNpgsql(_container.ConnectionString); });
            
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<TDbContext>();
            context.Database.EnsureCreated();
            
            services.AddScoped<SeedCreator>();
        });
    }
    
    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}