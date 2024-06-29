using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using Manager.Application;
using Manager.Infrastructure;
using Manager.Presentation.Middlewares;
using Manager.Setup;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Logging.AddOpenTelemetryLogging();
    builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApplicationDi()
    .AddServices()
    .AddPersistence(configuration)
    .AddInterceptors()
    .AddVersioning()
    .AddRedis(configuration)
    .AddOpenTelemetryServices()
    .Addbehaviours()
    .AddSwaggerConfig();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseDeveloperExceptionPage();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfig(provider);
app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.MapControllers();
app.AddServicesHealthChecks();
app.UseRouting();

app.UseCors(cp => cp
    .AllowAnyOrigin()
    .SetPreflightMaxAge(TimeSpan.FromHours(24))
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

app.Run();


public partial class Program {}