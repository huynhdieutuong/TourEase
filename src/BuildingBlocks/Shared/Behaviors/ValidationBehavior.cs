using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = BuildingBlocks.Shared.Exceptions.ValidationException;

namespace BuildingBlocks.Shared.Behaviors;
public class ValidationBehavior<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse>
    where TRequest : IRequest<TReponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TReponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
                              ILogger<ValidationBehavior<TRequest, TReponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(r => r.Errors.Any())
                                        .SelectMany(r => r.Errors)
                                        .ToList();

        if (failures.Count != 0)
        {
            foreach (var failure in failures)
            {
                _logger.LogError("Validation failed for {RequestType} on property '{PropertyName}' with value '{AttemptedValue}': {ErrorMessage}", typeof(TRequest).Name, failure.PropertyName, failure.AttemptedValue, failure.ErrorMessage);
            }
            throw new ValidationException(failures);
        }

        return await next();
    }
}
