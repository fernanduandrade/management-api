using FluentValidation;
using Shop.Application.Products.CreateProduct;

namespace Shop.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}