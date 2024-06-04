using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.Clients;

namespace Manager.Application.Clients.DeleteBulk;

public class DeleteClientsBulkCommandHandler : IRequestHandler<DeleteClientsBulkCommand, ApiResult<Unit>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IClientRepository _clientRepository;

  public DeleteClientsBulkCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
  {
    _clientRepository = clientRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<ApiResult<Unit>> Handle(DeleteClientsBulkCommand request, CancellationToken cancellationToken)
  {
    _clientRepository.DeleteBulk(request.Ids);
    await _unitOfWork.Commit(cancellationToken);

    return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
  }
}
