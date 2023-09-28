using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using eSlozka.Data;
using eSlozka.Domain.Exceptions;
using eSlozka.Domain.Extensions;
using eSlozka.Domain.Models;
using eSlozka.Domain.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSlozka.Core.Commands.Users;

public record RegisterCommand(string? GivenName, string? FamilyName, string? Email, string? Password, string? PasswordRepeat) : ICommand<RegisterResult>;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterResult>
{
    private static readonly Func<DataContext, string, bool> EmailTakenQuery = EF.CompileQuery((DataContext context, string email) => context.Users
        .AsNoTracking()
        .IgnoreQueryFilters()
        .Any(user => user.Email == email));

    private readonly IDbContextFactory<DataContext> _contextFactory;
    private readonly IHashProvider _hasher;
    private readonly ILogger<RegisterCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterCommandHandler(IDbContextFactory<DataContext> contextFactory, IMapper mapper, IHashProvider hasher, IValidator<RegisterCommand> validator, ILogger<RegisterCommandHandler> logger)
    {
        _contextFactory = contextFactory;
        _hasher = hasher;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
    }

    [SuppressMessage("ReSharper", "MethodHasAsyncOverloadWithCancellation", Justification = "SQLite does not support asynchronous operations. Entity Framework Core uses Task.FromResult as wrapper for action methods.")]
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var validationContext = new ValidationContext<RegisterCommand>(request);
        var validationErrors = (await _validator.ValidateAsync(validationContext, cancellationToken))
            .DistinctErrorsByProperty();

        if (validationErrors.Count > 0) return new RegisterResult(default, new EntityValidationException(validationErrors));

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var user = _mapper.Map<User>(request);

        (user.Password, user.PasswordSalt) = _hasher.GetHash(request.Password);

        context.Users.Add(user);
        context.SaveChanges();

        return new RegisterResult(user, default);
    }
}

public readonly record struct RegisterResult(User? User, EntityValidationException? ValidationException)
{
    public RegisterResultType Result => (User, ValidationException) switch
    {
        (not null, null) => RegisterResultType.Succeeded,
        (null, not null) => RegisterResultType.Failed,
        (_, _) => RegisterResultType.InternalServerError
    };
}

public enum RegisterResultType
{
    Succeeded,
    Failed,
    InternalServerError
}