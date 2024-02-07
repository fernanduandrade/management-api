using AutoMapper;
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
    private readonly IMapper _mapper;

    public CreateClientCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
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
        
        entity.Raise(new ClientCreateEvent(entity));
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        // if (entity.Id <= 0)
        //     return new ApiResult<ClientDTO>(null, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        ClientDTO dto = _mapper.Map<ClientDTO>(entity);
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}