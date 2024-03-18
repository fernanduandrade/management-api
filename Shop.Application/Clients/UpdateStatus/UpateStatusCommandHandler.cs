using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.UpdateStatus;

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
