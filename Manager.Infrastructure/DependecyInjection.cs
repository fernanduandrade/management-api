using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
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
    // public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    // {
    //     builder.Services.ConfigureHttpClientDefaults(http =>
    //     {
    //         // Turn on resilience by default
    //         http.AddStandardResilienceHandler();
    //         http.AddServiceDiscovery();
    //     });
    //     builder.Logging.AddOpenTelemetry(x =>
    //     {
    //         x.IncludeScopes = true;
    //         x.IncludeFormattedMessage = true;
    //     });
    //
    //     builder.Services.AddOpenTelemetry()
    //         .WithMetrics(x =>
    //         {
    //             x.AddRuntimeInstrumentation()
    //                 .AddMeter("Microsoft.AspNetCore.Hosting",
    //                     "Microsoft.AspNetCore.Server.Kestrel",
    //                     "System.Net.Http",
    //                     "Manager.API");
    //         })
    //         .WithTracing(x =>
    //         {
    //             x.AddAspNetCoreInstrumentation()
    //                 .AddConsoleExporter()
    //                 .AddHttpClientInstrumentation();
    //         });
    //     
    //     
    //     var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
    //     
    //     if (useOtlpExporter)
    //     {
    //         builder.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
    //         builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
    //         builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
    //     }
    //
    //     return builder;
    // }

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
