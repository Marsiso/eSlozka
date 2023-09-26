using eSlozka.Domain.Exceptions;
using eSlozka.Domain.Extensions;
using FluentValidation;
using MediatR;

namespace Cloud.Application.Validations;

public class FluentValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public FluentValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        ArgumentNullException.ThrowIfNull(validators);

        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();

        var validationContext = new ValidationContext<TRequest>(request);

        var validationResults = validators.Select(validator => validator.Validate(validationContext));

        var validationFailures = validationResults.DistinctErrorsByProperty();

        if (validationFailures.Count > 0) throw new EntityValidationException(validationFailures);

        return await next();
    }
}
