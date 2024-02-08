using FluentValidation;

namespace Shop.Application.SalesHistory.UpdateSaleHistory;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleHistoryCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ClientName).NotEmpty();
        RuleFor(x => x.ProductId).NotNull();
    }
}