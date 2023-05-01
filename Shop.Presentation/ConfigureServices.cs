

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Interfaces;
using Shop.Presentation.Services;

namespace Shop.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = typeof(Presentation.ConfigureServices).Assembly;
        services.AddControllers().AddApplicationPart(assembly);
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}