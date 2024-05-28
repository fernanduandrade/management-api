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
using StackExchange.Redis;

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

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
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

        services.AddSingleton<IConnectionMultiplexer>(options =>
            ConnectionMultiplexer.Connect(("127.0.0.1:6379")));
        
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
    //                     "Shop.WebApi");
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

    public static IServiceCollection ConfigureCache(this IServiceCollection services, IConfiguration configuration)
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
