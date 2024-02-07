using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedKernel;
using Shop.Application.Common.Interfaces;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence.Interceptors;
using Entities = Shop.Domain.Entities;
using Shop.Infrastructure.Identity;

namespace Shop.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _mediator= mediator;
        _auditableEntitySaveChangesInterceptor= auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Entities.Client> Clients => Set<Entities.Client>();
    
    public DbSet<Entities.Product> Products => Set<Entities.Product>();
    
    public DbSet<Entities.SalesHistory> Sales => Set<Entities.SalesHistory>();

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

    // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     await _mediator.DispatchDomainEvents(this);
    //     var result = await base.SaveChangesAsync(cancellationToken);
    //     return result;
    // }
}
