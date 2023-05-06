using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Client.DTOs;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Commands;

public sealed record UpdateClientCommand : IRequest<ApiResult<ClientDTO>>, IMapFrom<Entities.Client>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string LastName { get; init; }
    public bool IsActive { get; init; }
    public string Phone { get; init; }
    public decimal Debt { get; init; }
    public decimal Credit { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Client, UpdateClientCommand>();
    }
}

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ApiResult<ClientDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    
    public async Task<ApiResult<ClientDTO>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(client => client.Id == request.Id);

        if(entity is null)
            return new ApiResult<ClientDTO>(_mapper.Map<ClientDTO>(request), ResponseTypeEnum.Warning, "Failed to update the register.");

        var updateEntity = _mapper.Map<Entities.Client>(request);

        updateEntity.AddDomainEvent(new ClientCreateEvent(updateEntity));
        _context.Clients.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        
        ClientDTO dto = _mapper.Map<ClientDTO>(updateEntity);
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success ,"Operation completed successfully.");
    }
}