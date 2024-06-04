using Manager.Application.Common.Interfaces;
using Manager.Infrastructure.Persistence.Data;

namespace Manager.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
        => (_context) = (context);

    public async Task<bool> Commit(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken) > 0;

    public void Dispose()
    {
        _context.Dispose();
    }
}