using FluentValidation;
using Shop.Application.Client.Validations;
using Shop.Application.Sale.Commands;

namespace Shop.Application.Sale.Validations;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ClientId).NotNull();
        RuleFor(x => x.TotalPrice).NotEqual(0);
        RuleFor(x => x.ProductId).NotNull();
    }
}