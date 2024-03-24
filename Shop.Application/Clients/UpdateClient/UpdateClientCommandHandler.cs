using AutoMapper;
using MediatR;
using Shop.Application.Clients.Dtos;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.UpdateClient;

public sealed class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ApiResult<ClientDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IUnitOfWork unitOfWork)
        =>(_clientRepository, _mapper, _unitOfWork) = (clientRepository, mapper, unitOfWork);

    public async Task<ApiResult<ClientDto>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Client>(request);

        _clientRepository.SetEntityStateModified(entity);
        _clientRepository.Update(entity);
        await _unitOfWork.Commit(cancellationToken);

        var dto = _mapper.Map<ClientDto>(entity);
        
        return new ApiResult<ClientDto>(dto, ResponseTypeEnum.Success ,"Concluido com sucesso");
    }
}