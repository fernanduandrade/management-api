namespace Shop.Application.Client.Interfaces;

public interface IClientRepository
{
    Task<Domain.Entities.Client> GetByIdAsync(long id);
}