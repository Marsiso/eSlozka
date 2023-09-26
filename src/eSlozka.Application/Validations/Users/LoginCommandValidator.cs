using eSlozka.Core.Commands.Users;
using FluentValidation;

namespace eSlozka.Application.Validations.Users;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("ValidationUserEmailRequired")
            .MaximumLength(256)
            .WithMessage("ValidationUserEmailMaxLength")
            .EmailAddress()
            .WithMessage("ValidationUserEmailInvalidFormat");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("ValidationUserPasswordRequired")
            .MaximumLength(256)
            .WithMessage("ValidationUserPasswordMaxLength");
    }
}
