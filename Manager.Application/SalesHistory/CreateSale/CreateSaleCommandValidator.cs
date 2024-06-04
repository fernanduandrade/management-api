using FluentValidation;
using Manager.Application.SalesHistory.CreateSale;

namespace Manager.Application.SalesHistory.CreateSale;

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