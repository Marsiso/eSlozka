using eSlozka.Core.Commands.Users;
using eSlozka.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eSlozka.Application.Validations.Users;

public class LoginCommandValidator : AbstractValidator<VerifyCredentialsCommand>
{
    public LoginCommandValidator(IStringLocalizer localizer)
    {
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
            .MinimumLength(8)
            .WithMessage(localizer[Translations.Validation.User.Password.MinLength])
            .MaximumLength(256)
            .WithMessage(localizer[Translations.Validation.User.Password.MaxLength]);
    }
}