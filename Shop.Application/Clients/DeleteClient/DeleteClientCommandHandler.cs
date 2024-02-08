using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.DeleteClient;

public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ApiResult>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        => (_clientRepository, _unitOfWork) = (clientRepository, unitOfWork);
    public async Task<ApiResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientRepository.Remove(request.Id);
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult(true, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}