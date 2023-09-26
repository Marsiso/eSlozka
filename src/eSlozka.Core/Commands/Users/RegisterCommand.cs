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
    private readonly IEnumerable<IValidator<RegisterCommand>> _validators;

    public RegisterCommandHandler(IDbContextFactory<DataContext> contextFactory, IMapper mapper, IHashProvider hasher, IEnumerable<IValidator<RegisterCommand>> validators, ILogger<RegisterCommandHandler> logger)
    {
        _contextFactory = contextFactory;
        _hasher = hasher;
        _logger = logger;
        _mapper = mapper;
        _validators = validators;
    }

    public Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var validationContext = new ValidationContext<RegisterCommand>(request);
        var validationErrors = _validators.Select(validator => validator.Validate(validationContext))
            .DistinctErrorsByProperty();

        using var context = _contextFactory.CreateDbContext();

        if (EmailTakenQuery(context, request.Email)) validationErrors.TryAdd(nameof(request.Email), new[] { "ValidationUserEmailAlreadyTaken" });

        if (validationErrors.Count > 0)
            return Task.FromResult(new RegisterResult(
                RegisterResultType.Failed,
                default,
                new EntityValidationException(validationErrors)));

        var user = _mapper.Map<User>(request);

        (user.Password, user.PasswordSalt) = _hasher.GetHash(request.Password);

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(new RegisterResult(
            RegisterResultType.Succeeded,
            user,
            default));
    }
}

public record struct RegisterResult(RegisterResultType Result, User? User, EntityValidationException? ValidationException);

public enum RegisterResultType
{
    Succeeded,
    Failed
}
