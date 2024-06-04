using MediatR;
using Manager.Application.Common.Interfaces;
using Manager.Application.Common.Models;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.DeleteSaleHistory;

public sealed class DeleteSaleHistoryCommandHandler : IRequestHandler<DeleteSaleHistoryCommand, ApiResult<Unit>>
{
    private readonly ISaleHistoryRepository _saleHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleHistoryCommandHandler(IUnitOfWork unitOfWork, ISaleHistoryRepository saleHistoryRepository)
        => (_unitOfWork, _saleHistoryRepository) = (unitOfWork, saleHistoryRepository);
    public async Task<ApiResult<Unit>> Handle(DeleteSaleHistoryCommand request, CancellationToken cancellationToken)
    {
        await _saleHistoryRepository.Remove(request.Id);
        await _unitOfWork.Commit(cancellationToken);

        return new ApiResult<Unit>(Unit.Value, ResponseTypeEnum.Success, "Error while trying to delete the register.");
    }
}