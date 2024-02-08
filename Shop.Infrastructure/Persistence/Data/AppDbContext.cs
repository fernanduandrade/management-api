using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedKernel;
using Shop.Domain.Clients;
using Shop.Domain.Products;
using Shop.Domain.SalesHistory;
using Shop.Infrastructure.Persistence.Interceptors;
using Shop.Infrastructure.Identity;

namespace Shop.Infrastructure.Persistence.Data;

public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options, operationalStoreOptions)
    {
        _mediator= mediator;
        _auditableEntitySaveChangesInterceptor= auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<SaleHistory> SalesHistory => Set<SaleHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Ignore<List<IDomainEvent>>().ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}
