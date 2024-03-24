using AutoMapper;
using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.GetAllClientPaginated;

public sealed class GetAllClientPaginatedQueryHandler
    : IRequestHandler<GetAllClientPaginatedQuery, ApiResult<PaginatedList<ClientDto>>>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetAllClientPaginatedQueryHandler(IMapper mapper, IClientRepository clientRepository)
        => (_mapper, _clientRepository) = (mapper, clientRepository);
    public async Task<ApiResult<PaginatedList<ClientDto>>> Handle(GetAllClientPaginatedQuery request,
            CancellationToken cancellationToken)
    {       
        var records = _clientRepository.GetAllPaginated();
        var pagination = await PaginatedList<ClientDto>
            .CreateAsync(records, request.PageNumber, request.PageSize, _mapper);
        
        return new ApiResult<PaginatedList<ClientDto>>(pagination,
            ResponseTypeEnum.Success ,message: "Concluido com sucesso.");
    }
}