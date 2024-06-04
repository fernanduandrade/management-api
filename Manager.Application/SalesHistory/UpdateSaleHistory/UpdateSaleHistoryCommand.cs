using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.UpdateSaleHistory;

public record UpdateSaleHistoryCommand(Guid Id,
    DateTime Date,
    string ClientName,
    int Quantity,
    decimal PricePerUnit,
    PaymentType PaymentType,
    Guid ProductId) : IRequest<ApiResult<SaleHistoryDto>> {}