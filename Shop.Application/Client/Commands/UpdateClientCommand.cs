using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Commands;

public sealed record UpdateClientCommand : IRequest<ApiResult<ClientDTO>>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public bool IsActive { get; init; }
    public string Phone { get; init; }
    public decimal Debt { get; init; }
    public decimal Credit { get; init; }
}

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ApiResult<ClientDTO>>
{
    private readonly IAppDbContext _context;

    public UpdateClientCommandHandler(IAppDbContext context)
        => (_context) = (context);
    
    public async Task<ApiResult<ClientDTO>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(client => client.Id == request.Id);

        if(entity is null)
            return new ApiResult<ClientDTO>(new ClientDTO(), ResponseTypeEnum.Warning, "Failed to update the register.");

        Entities.Client updateEntity = new()
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

        ClientDTO dto = new()
        {
            Id = updateEntity.Id,
            Credit = updateEntity.Credit,	
            Name = updateEntity.Name,	
            IsActive = updateEntity.IsActive,	
            LastName = updateEntity.LastName,	
            Phone = updateEntity.Phone,	
            Debt = updateEntity.Debt,
        };
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success ,"Operation completed successfully.");
    }
}