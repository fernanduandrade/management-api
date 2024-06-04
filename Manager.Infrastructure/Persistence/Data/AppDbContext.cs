using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedKernel;
using Manager.Domain.Clients;
using Manager.Domain.OrderProducts;
using Manager.Domain.Orders;
using Manager.Domain.Products;
using Manager.Domain.SalesHistory;
using Manager.Infrastructure.Persistence.Interceptors;
using Manager.Infrastructure.Identity;

namespace Manager.Infrastructure.Persistence.Data;

public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options, operationalStoreOptions)
    {
        _mediator= mediator;
        _auditableEntitySaveChangesInterceptor= auditableEntitySaveChangesInterceptor;
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<SaleHistory> SalesHistory => Set<SaleHistory>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Ignore<List<IDomainEvent>>().ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}
