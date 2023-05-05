using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;

namespace Shop.Application.Client.Commands;

public sealed record UpdateClientCommand : IRequest<ApiResult<bool>>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public bool IsActive { get; init; }
    public string Phone { get; init; }
    public decimal Debt { get; init; }
    public decimal Credit { get; init; }
}

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateClientCommandHandler(IAppDbContext context)
        => (_context) = (context);
    
    public async Task<ApiResult<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(client => client.Id == request.Id);

        if(entity is null)
            return new ApiResult<bool>(false, "Falha ao executar a operação");
    
        Domain.Entities.Client updateEntity = new()
        {
            Id = request.Id,
            Credit = request.Credit,
            Name = request.Name,
            IsActive = request.IsActive,
            LastName = request.LastName,
            Phone = request.Phone,
            Debt = request.Debt,
        };

        updateEntity.AddDomainEvent(new ClientCreateEvent(updateEntity));
        _context.Clients.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}