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
        => (_clientRepository, _mapper) = (clientRepository, mapper);
    public async Task<ApiResult<PaginatedList<ClientDto>>> Handle(GetAllClientPaginatedQuery request,
            CancellationToken cancellationToken)
    {
        var query = _clientRepository.GetAllPaginated();
        var pagination = await PaginatedList<Client>
            .CreateAsync(query, request.PageNumber, request.PageSize);
        
        var dto = _mapper.Map<List<ClientDto>>(pagination.Items);
        var result = new PaginatedList<ClientDto>(dto, pagination.TotalCount, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<ClientDto>>(result,
            ResponseTypeEnum.Success ,message: "Operation completed successfully.");
    }
}