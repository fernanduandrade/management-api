using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;

namespace Shop.Application.Client.Commands;

public sealed record CreateClientCommand : IRequest<ApiResult<bool>>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
    public decimal Debt { get; set; }
    public decimal Credit { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public CreateClientCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult<bool>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Client entity = new()
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

        return new ApiResult<bool>(true);
    }
}