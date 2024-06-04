using MediatR;
using Manager.Application.Common.Models;
using Manager.Application.SalesHistory.Dtos;
using Manager.Domain.SalesHistory;

namespace Manager.Application.SalesHistory.CreateSale;

public sealed record CreateSaleHistoryCommand(DateTime Date,
    string ClientName,
    Guid ProductId,
    int Quantity,
    decimal PricePerUnit,
    PaymentType PaymentType) : IRequest<ApiResult<SaleHistoryDto>> {}