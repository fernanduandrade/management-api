using Microsoft.IdentityModel.Logging;
using Manager.OAuth.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityServerConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
    .AddTestUsers(IdentityServerConfiguration.TestUsers)
    .AddDeveloperSigningCredential();

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
IdentityModelEventSource.ShowPII = true;

builder.Services.AddSingleton(loggerFactory);
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");
app.UseRouting();

app.UseIdentityServer();
app.Run();