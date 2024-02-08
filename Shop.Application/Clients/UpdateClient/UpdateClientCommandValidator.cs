using FluentValidation;
using Shop.Application.Clients.UpdateClient;

namespace Shop.Application.Clients.Validations;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
    }
}