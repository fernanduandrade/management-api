using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Manager.Domain.Clients;
namespace Manager.Infrastructure.Persistence.Data.Repositories;


public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Client> _dbSet;
    private readonly IRepository<Client> _repository;


    public ClientRepository(AppDbContext context, IRepository<Client> repository)
    {
        _context = context;
        _dbSet = _context.Set<Client>();
        _repository = repository;

    }
    public async Task<Client> FindByIdAsync(Guid id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public void Add(Client client)
        => _repository.Add(client);

    public void Update(Client client)
        => _repository.Update(client);

    public async Task Remove(Guid id)
    {
        await _repository.Remove(id);
    }

    public void DeleteBulk(List<Guid> ids)
    {
        _repository.DeleteBulk(ids);
    }

    public IQueryable<Client> Get(Expression<Func<Client, bool>>? filter = null, bool readOnly = true)
    {
        return _repository.GetAll(filter, readOnly);
    }
}