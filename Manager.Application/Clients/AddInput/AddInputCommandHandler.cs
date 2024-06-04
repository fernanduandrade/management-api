using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.AddInput;

public sealed class AddInputCommandHandler(IUnitOfWork unitOfWork,
  IClientRepository clientRepository) : IRequestHandler<AddInputCommand, ApiResult<Unit>>
{
    public async Task<ApiResult<Unit>> Handle(AddInputCommand request, CancellationToken cancellationToken)
    {
      var client =  await clientRepository.FindByIdAsync(request.id);

      if(request.type == InputType.Debito)
        client.SetDebtInput(request.value);
      else
        client.SetCreditInput(request.value);
      
      clientRepository.Update(client);
      await unitOfWork.Commit(cancellationToken);

      return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
    }
}
