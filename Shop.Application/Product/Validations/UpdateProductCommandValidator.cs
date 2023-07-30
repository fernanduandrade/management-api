using FluentValidation;
using Shop.Application.Product.Commands;

namespace Shop.Application.Product.Validations;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}