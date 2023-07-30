using FluentValidation;
using Shop.Application.Product.Commands;

namespace Shop.Application.Product.Validations;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}