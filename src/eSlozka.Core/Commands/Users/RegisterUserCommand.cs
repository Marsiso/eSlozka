using AutoMapper;
using eSlozka.Data;
using eSlozka.Domain.Exceptions;
using eSlozka.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSlozka.Core.Commands.Users;

public record RegisterUserCommand(string? GivenName, string? FamilyName, string? Email, string? Password, string? PasswordRepeat) : IRequest<RegisterUserResult>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IDbContextFactory<DataContext> _contextFactory;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEnumerable<IValidator<RegisterUserCommand>> _validators;

    public Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var validationContext = new ValidationContext<RegisterUserCommand>(request);

        throw new NotImplementedException();
    }
}

public record struct RegisterUserResult(RegisterUserResultType Result, User? User, EntityValidationException? ValidationException);

public enum RegisterUserResultType
{
    Succeeded,
    Failed
}
