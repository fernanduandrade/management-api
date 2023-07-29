using AutoMapper;
using MediatR;
using Shop.Application.Client.DTOs;
using Shop.Application.Client.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Client.Queries;

public sealed record GetClientByIdQuery : IRequest<ApiResult<ClientDTO>>
{
    public long Id { get; init; }
}

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery ,ApiResult<ClientDTO>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        => (_clientRepository, _mapper) = (clientRepository, mapper);
    public async Task<ApiResult<ClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _clientRepository.GetByIdAsync(request.Id);

        if(result is null)
            return new ApiResult<ClientDTO>(null, ResponseTypeEnum.Warning,"Failed to find the record.");

        var dto = _mapper.Map<ClientDTO>(result);
        
        return new ApiResult<ClientDTO>(dto, ResponseTypeEnum.Success,"Operation completed successfully.");
    }
}