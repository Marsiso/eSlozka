using AutoMapper;
using eSlozka.Data;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Exceptions;
using eSlozka.Domain.Extensions;
using eSlozka.Domain.Models;
using eSlozka.Domain.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSlozka.Core.Commands.Users;

public record LoginCommand(string? Email, string? Password) : ICommand<LoginResult>;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
{
    private static readonly Func<DataContext, string, User?> EmailTakenQuery = EF.CompileQuery((DataContext context, string email) => context.Users
        .AsNoTracking()
        .SingleOrDefault(user => user.Email == email));

    private readonly IDbContextFactory<DataContext> _contextFactory;
    private readonly IHashProvider _hasher;
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEnumerable<IValidator<LoginCommand>> _validators;

    public LoginCommandHandler(IDbContextFactory<DataContext> contextFactory, IMapper mapper, IHashProvider hasher, IEnumerable<IValidator<LoginCommand>> validators, ILogger<LoginCommandHandler> logger)
    {
        _contextFactory = contextFactory;
        _hasher = hasher;
        _logger = logger;
        _mapper = mapper;
        _validators = validators;
    }

    public Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var validationContext = new ValidationContext<LoginCommand>(request);
        var validationErrors = _validators.Select(validator => validator.Validate(validationContext))
            .DistinctErrorsByProperty();

        using var context = _contextFactory.CreateDbContext();

        var user = EmailTakenQuery(context, request.Email);

        if (user is null) validationErrors.TryAdd(nameof(request.Email), new[] { "ValidationUserEmailOrPasswordInvalid" });

        if (validationErrors.Count > 0)
            return Task.FromResult(new LoginResult(
                LoginResultType.Failed,
                default,
                new EntityValidationException(validationErrors)));

        var verificationResult = _hasher.VerifyHash(request.Password, user.Password, user.PasswordSalt);

        if (verificationResult == HashVerificationResult.Failed)
        {
            validationErrors.TryAdd(nameof(request.Email), new[] { "ValidationUserEmailOrPasswordInvalid" });

            return Task.FromResult(new LoginResult(
                LoginResultType.Failed,
                default,
                new EntityValidationException(validationErrors)));
        }

        return Task.FromResult(new LoginResult(
            LoginResultType.Succeeded,
            user,
            default));
    }
}

public record struct LoginResult(LoginResultType Result, User? User, EntityValidationException? ValidationException);

public enum LoginResultType
{
    Succeeded,
    Failed
}
