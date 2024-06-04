using FluentValidation;

namespace Manager.Application.SalesHistory.UpdateSaleHistory;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleHistoryCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ClientName).NotEmpty();
        RuleFor(x => x.ProductId).NotNull();
        RuleFor(x => x.PaymentType).NotNull();
    }
}