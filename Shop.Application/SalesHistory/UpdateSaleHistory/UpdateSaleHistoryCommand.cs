using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.UpdateSaleHistory;

public record UpdateSaleHistoryCommand(Guid Id,
    DateTime Date,
    string ClientName,
    int Quantity,
    decimal PricePerUnit,
    PaymentType PaymentType,
    Guid ProductId) : IRequest<ApiResult<SaleHistoryDto>> {}