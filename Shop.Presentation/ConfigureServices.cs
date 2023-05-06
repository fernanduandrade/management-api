

using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Application.Common.Interfaces;
using Shop.Presentation.Services;
using Shop.Presentation.Setup;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shop.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerSetup>();
        var assembly = typeof(Presentation.ConfigureServices).Assembly;
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(assembly);
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddHttpContextAccessor();
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        
        return services;
    }
}