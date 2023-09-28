using eSlozka.Core.Commands.Users;
using eSlozka.Core.Queries.Users;
using eSlozka.Domain.Constants;
using eSlozka.Domain.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace eSlozka.Application.Validations.Users;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IStringLocalizer _localizer;
    private readonly ISender _sender;

    public RegisterCommandValidator(ISender sender, IStringLocalizer localizer)
    {
        _sender = sender;
        _localizer = localizer;

        RuleFor(command => command.GivenName)
            .NotEmpty()
            .WithMessage(_localizer[Translations.Validation.User.GivenName.Required])
            .MaximumLength(256)
            .WithMessage(_localizer[Translations.Validation.User.GivenName.MaxLength]);

        RuleFor(command => command.FamilyName)
            .NotEmpty()
            .WithMessage(_localizer[Translations.Validation.User.FamilyName.Required])
            .MaximumLength(256)
            .WithMessage(_localizer[Translations.Validation.User.FamilyName.MaxLength]);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage(_localizer[Translations.Validation.User.Email.Required])
            .MaximumLength(256)
            .WithMessage(_localizer[Translations.Validation.User.Email.MaxLength])
            .EmailAddress()
            .WithMessage(_localizer[Translations.Validation.User.Email.Format])
            .MustAsync(async (email, cancellation) => await EmailAddressExistsNot(email, cancellation))
            .WithMessage(_localizer[Translations.Validation.User.Email.Exists]);

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage(_localizer[Translations.Validation.User.Password.Required])
            .MinimumLength(8)
            .WithMessage(_localizer[Translations.Validation.User.Password.MinLength])
            .MaximumLength(256)
            .WithMessage(_localizer[Translations.Validation.User.Password.MaxLength])
            .HasNumericCharacter()
            .WithMessage(_localizer[Translations.Validation.User.Password.NumericCharacter])
            .HasSpecialCharacter()
            .WithMessage(_localizer[Translations.Validation.User.Password.SpecialCharacter])
            .HasLowerCaseCharacter()
            .WithMessage(_localizer[Translations.Validation.User.Password.LowerCaseCharacter])
            .HasUpperCaseCharacter()
            .WithMessage(_localizer[Translations.Validation.User.Password.UpperCaseCharacter]);

        RuleFor(command => command.PasswordRepeat)
            .NotEmpty()
            .WithMessage(_localizer[Translations.Validation.User.PasswordRepetition.Required])
            .MaximumLength(256)
            .WithMessage(_localizer[Translations.Validation.User.PasswordRepetition.MaxLength]);

        When(command => !string.IsNullOrWhiteSpace(command.Password) && !string.IsNullOrWhiteSpace(command.PasswordRepeat), () =>
        {
            RuleFor(command => command.PasswordRepeat)
                .Must((command, passwordRepetition) => passwordRepetition?.Equals(command.Password) ?? false)
                .WithMessage(_localizer[Translations.Validation.User.PasswordRepetition.Match]);
        });
    }

    private async Task<bool> EmailAddressExistsNot(string? email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email)) return true;

        var query = new EmailExistsQuery(email);
        var emailExists = await _sender.Send(query, cancellationToken);

        return !emailExists;
    }
}