using MediatR;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Commands;

public sealed record CreateClientCommand : IRequest<ApiResult<ClientDTO>>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
    public decimal Debt { get; set; }
    public decimal Credit { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ApiResult<ClientDTO>>
{
    private readonly IAppDbContext _context;

    public CreateClientCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult<ClientDTO>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Entities.Client entity = new()
        {
            Credit = request.Credit,	
            Name = request.Name,	
            IsActive = request.IsActive,	
            LastName = request.LastName,	
            Phone = request.Phone,	
            Debt = request.Debt,
        };
        
        entity.AddDomainEvent(new ClientCreateEvent(entity));
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        ClientDTO dto = new()
        {
            Id = entity.Id,
            Credit = entity.Credit,	
            Name = entity.Name,	
            IsActive = entity.IsActive,	
            LastName = entity.LastName,	
            Phone = entity.Phone,	
            Debt = entity.Debt,
        };
        
        if (entity.Id <= 0)
            return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}