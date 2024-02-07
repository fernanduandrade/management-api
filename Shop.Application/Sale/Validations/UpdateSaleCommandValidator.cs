using FluentValidation;
using Shop.Application.Sale.Commands;

namespace Shop.Application.Sale.Validations;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ClientId).NotNull();
        RuleFor(x => x.TotalPrice).NotEqual(0);
        RuleFor(x => x.ProductId).NotNull();
    }
}