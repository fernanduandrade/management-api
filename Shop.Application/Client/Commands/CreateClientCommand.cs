using AutoMapper;
using MediatR;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Commands;

public sealed record CreateClientCommand : IRequest<ApiResult<ClientDTO>>, IMapFrom<Entities.Client>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
    public decimal Debt { get; set; }
    public decimal Credit { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Client, CreateClientCommand>();
    }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ApiResult<ClientDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateClientCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<ClientDTO>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.Client>(request);
        
        entity.AddDomainEvent(new ClientCreateEvent(entity));
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        ClientDTO dto = _mapper.Map<ClientDTO>(entity);
        
        if (entity.Id <= 0)
            return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}