using eSlozka.Core.Commands.Users;
using eSlozka.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eSlozka.Application.Validations.Users;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IStringLocalizer localizer)
    {
        RuleFor(command => command.GivenName)
            .NotEmpty()
            .WithMessage(localizer[Translations.Validation.User.GivenName.Required])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.GivenName.MaxLength]);

        RuleFor(command => command.FamilyName)
            .NotEmpty()
            .WithMessage(localizer[Translations.Validation.User.FamilyName.Required])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.FamilyName.MaxLength]);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage(localizer[Translations.Validation.User.Email.Required])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.Email.MaxLength])
            .EmailAddress()
            .WithMessage(localizer[Translations.Validation.User.Email.Format]);

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage(localizer[Translations.Validation.User.Password.Required])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.Password.MaxLength]);

        RuleFor(command => command.PasswordRepeat)
            .NotEmpty()
            .WithMessage(localizer[Translations.Validation.User.PasswordRepetition.Required])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.PasswordRepetition.MaxLength]);

        When(command => !string.IsNullOrWhiteSpace(command.Password) && !string.IsNullOrWhiteSpace(command.PasswordRepeat), () =>
        {
            RuleFor(command => command.PasswordRepeat)
                .Must((command, passwordRepetition) => passwordRepetition?.Equals(command.Password) ?? false)
                .WithMessage(localizer[Translations.Validation.User.PasswordRepetition.Match]);
        });
    }
}