using AutoMapper;
using MediatR;
using Shop.Application.Client.DTOs;
using Shop.Application.Client.Interfaces;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Client.Commands;

public sealed record UpdateClientCommand : IRequest<ApiResult<ClientDTO>>
{
    public Guid Id { get; init; }
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
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IAppDbContext context, IClientRepository repository, IMapper mapper)
        => (_context, _repository, _mapper) = (context, repository, mapper);
    
    public async Task<ApiResult<ClientDTO>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindByIdAsync(request.Id);

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

        updateEntity.Raise(new ClientCreateEvent(updateEntity));
        _repository.SetEntityStateModified(updateEntity);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<ClientDTO>(updateEntity);
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success ,"Operation completed successfully.");
    }
}