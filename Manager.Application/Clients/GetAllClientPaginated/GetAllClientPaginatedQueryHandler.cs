using AutoMapper;
using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.GetAllClientPaginated;

public sealed class GetAllClientPaginatedQueryHandler(IMapper mapper, IClientRepository clientRepository)
    : IRequestHandler<GetAllClientPaginatedQuery, ApiResult<PaginatedList<ClientDto>>>
{
    public async Task<ApiResult<PaginatedList<ClientDto>>> Handle(GetAllClientPaginatedQuery request,
            CancellationToken cancellationToken)
    {       
        var records = clientRepository
            .Get()
            .OrderByDescending(x => x.Created);;
        var pagination = await PaginatedList<ClientDto>
            .CreateAsync(records, request.PageNumber, request.PageSize, mapper);
        
        return new ApiResult<PaginatedList<ClientDto>>(pagination,
            ResponseTypeEnum.Success ,message: "Concluido com sucesso.");
    }
}