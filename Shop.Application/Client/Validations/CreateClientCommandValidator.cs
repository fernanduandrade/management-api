using FluentValidation;
using Shop.Application.Client.Commands;

namespace Shop.Application.Client.Validations;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
        RuleFor(x => x.Phone).NotEmpty();
    }
}