using AutoMapper;
using MediatR;
using Manager.Application.Clients.Dtos;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.CreateClient;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ApiResult<ClientDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResult<ClientDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = Client.Create(request.Name, request.LastName, request.Phone, request.IsActive,
                request.Debt, request.Credit);

            _clientRepository.Add(entity);
            await _unitOfWork.Commit(cancellationToken);

            var dto = _mapper.Map<ClientDto>(entity);
            return new ApiResult<ClientDto>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}