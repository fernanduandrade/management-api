using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.SalesHistory.Dtos;
using Shop.Domain.SalesHistory;

namespace Shop.Application.SalesHistory.CreateSale;

public sealed record CreateSaleHistoryCommand(DateTime Date,
    string ClientName,
    Guid ProductId,
    int Quantity,
    decimal PricePerUnit,
    PaymentType PaymentType) : IRequest<ApiResult<SaleHistoryDto>> {}