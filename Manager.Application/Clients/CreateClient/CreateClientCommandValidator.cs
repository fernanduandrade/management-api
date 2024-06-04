using FluentValidation;

namespace Manager.Application.Clients.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
        RuleFor(x => x.Phone).NotEmpty();
    }
}