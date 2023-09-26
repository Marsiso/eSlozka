using eSlozka.Core.Commands.Users;
using FluentValidation;

namespace eSlozka.Application.Validations.Users;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.GivenName)
            .NotEmpty()
            .WithMessage("ValidationUserGivenNameRequired")
            .MaximumLength(256)
            .WithMessage("ValidationUserGivenNameMaxLength");

        RuleFor(command => command.FamilyName)
            .NotEmpty()
            .WithMessage("ValidationUserFamilyNameRequired")
            .MaximumLength(256)
            .WithMessage("ValidationUserFamilyNameMaxLength");

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

        RuleFor(command => command.PasswordRepeat)
            .NotEmpty()
            .WithMessage("ValidationUserPasswordRepetitionRequired")
            .MaximumLength(256)
            .WithMessage("ValidationUserPasswordRepetitionMaxLength");

        When(command => !string.IsNullOrWhiteSpace(command.Password) && !string.IsNullOrWhiteSpace(command.PasswordRepeat), () =>
        {
            RuleFor(command => command.PasswordRepeat)
                .Must((command, passwordRepetition) => passwordRepetition.Equals(command.Password))
                .WithMessage("ValidationUserPasswordRepetitionDoesNotMatch");
        });
    }
}
