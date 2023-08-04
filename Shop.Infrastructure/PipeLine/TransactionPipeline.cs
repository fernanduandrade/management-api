using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.PipeLine;

public class TransactionPipeline
{
    private readonly AppDbContext _context;

    public TransactionPipeline(AppDbContext context)
    {
        _context = context;
    }

    public async Task OnCommandAsync<TCommand>(Func<TCommand, CancellationToken, Task> next, TCommand command,
        CancellationToken ct)
    {
        await using var tx = await _context.Database.BeginTransactionAsync(ct);

        await next(command, ct);
        await _context.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);
    }
    public async Task<TResult> OnCommandAsync<TCommand, TResult>(Func<TCommand, CancellationToken, Task<TResult>> next, TCommand cmd, CancellationToken ct)
    {
        await using var tx = await _context.Database.BeginTransactionAsync(ct);

        var result = await next(cmd, ct);

        await _context.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return result;
    }
    
}