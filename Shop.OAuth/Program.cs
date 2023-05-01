using Shop.OAuth.Setup;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World");
    });
});
app.UseAuthorization();

app.MapRazorPages();

app.Run();