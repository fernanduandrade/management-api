namespace Shop.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken);
}