using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using SharedKernel;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Clients;
using Manager.Domain.OrderProducts;
using Manager.Domain.Orders;
using Manager.Domain.Products;
using Manager.Domain.SalesHistory;
using Manager.Infrastructure.Common;
using Manager.Infrastructure.Persistence.Data;
using Manager.Infrastructure.Persistence.Data.Repositories;
using Manager.Infrastructure.Persistence.Interceptors;
using Manager.Infrastructure.Services;
using OpenTelemetry.Resources;

namespace Manager.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IUserManagerService, UserManagerService>();
        return services;
    }

    public static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connection = Environment.GetEnvironmentVariable("DB_CONNECTION");
            options.UseNpgsql(connection , 
                config =>
                {
                    config.EnableRetryOnFailure(3);
                });
        });

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ISaleHistoryRepository, SaleHistoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

    public static ILoggingBuilder AddOpenTelemetryLogging(this ILoggingBuilder logging)
    {

        logging.AddOpenTelemetry(x =>
        {
            x.IncludeScopes = true;
            x.IncludeFormattedMessage = true;
            x.AddOtlpExporter();
        });
    
        return logging;
    }

    public static IServiceCollection AddOpenTelemetryServices(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("ManagerAPI"))
            .WithMetrics(x =>
            {
                x
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation();

                x.AddOtlpExporter();
            })
            .WithTracing(x =>
            {
                x
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddHttpClientInstrumentation();

                x.AddOtlpExporter();
            });
        
    
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:Server"];
            options.InstanceName = configuration["Redis:Instance"];
        });
        
        services.AddScoped<ICacheService, CacheService>();
        return services;
    }
}
