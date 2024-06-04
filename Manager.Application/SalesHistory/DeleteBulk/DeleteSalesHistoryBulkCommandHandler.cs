using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.DeleteBulk;

public class DeleteSalesHistoryBulkCommandHandler : IRequestHandler<DeleteSalesHistoryBulkCommand, ApiResult<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISaleHistoryRepository _saleHistoryRepository;

    public DeleteSalesHistoryBulkCommandHandler(IUnitOfWork unitOfWork, ISaleHistoryRepository saleHistoryRepository)
    {
        _unitOfWork = unitOfWork;
        _saleHistoryRepository = saleHistoryRepository;
    }
    public async Task<ApiResult<Unit>> Handle(DeleteSalesHistoryBulkCommand request, CancellationToken cancellationToken)
    {
      _saleHistoryRepository.DeleteBulk(request.Ids);
      await _unitOfWork.Commit(cancellationToken);

      return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success);
    }
}
