using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.UpdateStatus;

public sealed class UpateStatusCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
    : IRequestHandler<UpdateStatusCommand, ApiResult<Unit>>
{
    public async Task<ApiResult<Unit>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.FindByIdAsync(request.id);
        client.ChangeStatus();
        await unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
    }
}
