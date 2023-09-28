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

public record VerifyCredentialsCommand(string? Email, string? Password) : ICommand<VerifyCredentialsResult>;

public class VerifyCredentialsCommandHandler : ICommandHandler<VerifyCredentialsCommand, VerifyCredentialsResult>
{
    private static readonly Func<DataContext, string, User?> EmailTakenQuery = EF.CompileQuery((DataContext context, string email) => context.Users
        .AsNoTracking()
        .SingleOrDefault(user => user.Email == email));

    private readonly IDbContextFactory<DataContext> _contextFactory;
    private readonly IHashProvider _hasher;
    private readonly ILogger<VerifyCredentialsCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<VerifyCredentialsCommand> _validator;

    public VerifyCredentialsCommandHandler(IDbContextFactory<DataContext> contextFactory, IMapper mapper, IHashProvider hasher, IValidator<VerifyCredentialsCommand> validator, ILogger<VerifyCredentialsCommandHandler> logger)
    {
        _contextFactory = contextFactory;
        _hasher = hasher;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
    }

    public Task<VerifyCredentialsResult> Handle(VerifyCredentialsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var validationContext = new ValidationContext<VerifyCredentialsCommand>(request);
        var validationErrors = _validator.Validate(validationContext)
            .DistinctErrorsByProperty();

        using var context = _contextFactory.CreateDbContext();

        var user = EmailTakenQuery(context, request.Email!);

        if (user is null) validationErrors.TryAdd(nameof(request.Email), new[] { "ValidationUserEmailOrPasswordInvalid" });

        if (validationErrors.Count > 0) return Task.FromResult(new VerifyCredentialsResult(default, new EntityValidationException(validationErrors)));

        var verificationResult = _hasher.VerifyHash(request.Password, user.Password, user.PasswordSalt);

        if (verificationResult == HashVerificationResult.Failed)
        {
            validationErrors.TryAdd(nameof(request.Email), new[] { "ValidationUserEmailOrPasswordInvalid" });

            return Task.FromResult(new VerifyCredentialsResult(default, new EntityValidationException(validationErrors)));
        }

        return Task.FromResult(new VerifyCredentialsResult(user, default));
    }
}

public readonly record struct VerifyCredentialsResult(User? User, EntityValidationException? ValidationException)
{
    public VerifyCredentialsResultType Result => (User, ValidationException) switch
    {
        (not null, null) => VerifyCredentialsResultType.Succeeded,
        (null, not null) => VerifyCredentialsResultType.Failed,
        (_, _) => VerifyCredentialsResultType.InternalServerError
    };
}

public enum VerifyCredentialsResultType
{
    Succeeded,
    Failed,
    InternalServerError
}