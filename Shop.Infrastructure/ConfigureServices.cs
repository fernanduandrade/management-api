using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Clients;
using Shop.Domain.OrderProducts;
using Shop.Domain.Orders;
using Shop.Domain.Products;
using Shop.Domain.SalesHistory;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence.Data;
using Shop.Infrastructure.Persistence.Data.Repositories;
using Shop.Infrastructure.Persistence.Interceptors;
using Shop.Infrastructure.PipeLine;
using Shop.Infrastructure.Services;

namespace Shop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                config =>
                {
                    config.EnableRetryOnFailure(3);
                });
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ISaleHistoryRepository, SaleHistoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

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
