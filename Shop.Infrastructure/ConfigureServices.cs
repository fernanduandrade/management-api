using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Client.Interfaces;
using Shop.Application.Common.Interfaces;
using Shop.Application.Product.Interfaces;
using Shop.Application.Sale.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Persistence.Interceptors;
using Shop.Infrastructure.Persistence.Repositories;
using Shop.Infrastructure.PipeLine;
using Shop.Infrastructure.Services;

namespace Shop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("ShopApp"));
        }
        else
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddMediator(o =>
        {
            o.AddPipeline<TransactionPipeline>();
            o.AddPipelineForEFCoreTransaction<AppDbContext>(option =>
            {
                option.BeginTransactionOnCommand = true;
            });
        });
        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();
        return services;
    }
}
