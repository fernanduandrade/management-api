using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Clients;

namespace Shop.Application.Clients.DeleteBulk;

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
