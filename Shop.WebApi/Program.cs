using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Shop.Application;
using Shop.Infrastructure;
using Shop.Presentation;
using Shop.Presentation.Middlewares;
using Shop.Presentation.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation()
    .AddSwaggerConfig();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:5211";
        options.Audience = "cwm.client";
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters.ValidateAudience = false;
        options.RequireHttpsMetadata = false;
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                return Task.CompletedTask;
            }
        };
    });

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerConfig(provider);
}
app.MapControllers();

app.UseHealthChecks("/status", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
    ResponseWriter = async (context, report) =>
    {
        var result = JsonSerializer.Serialize(
            new
            {
                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                statusApplication = report.Status.ToString(),
            });
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});
app.UseRouting();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .SetPreflightMaxAge(TimeSpan.FromHours(24))
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.Run();


public partial class Program {}