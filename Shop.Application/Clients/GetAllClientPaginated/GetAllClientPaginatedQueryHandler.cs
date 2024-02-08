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
        var result = await _clientRepository.GetAllPaginated(request.PageSize, request.PageNumber);
        var dtos = _mapper.Map<List<ClientDto>>(result);
        var paginate = new PaginatedList<ClientDto>(dtos, dtos.Count, request.PageNumber, request.PageSize);
        return new ApiResult<PaginatedList<ClientDto>>(paginate,
            ResponseTypeEnum.Success ,message: "Operation completed successfully.");
    }
}