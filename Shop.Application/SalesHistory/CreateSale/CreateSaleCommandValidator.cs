using FluentValidation;
using Shop.Application.SalesHistory.CreateSale;

namespace Shop.Application.SalesHistory.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleHistoryCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ClientName).NotEmpty();
        RuleFor(x => x.ProductId).NotNull();
        RuleFor(x => x.PaymentType).NotNull();
    }
}