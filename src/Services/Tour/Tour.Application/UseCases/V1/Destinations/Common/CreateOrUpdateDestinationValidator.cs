using FluentValidation;
using Tour.Domain.Entities.Enums;

namespace Tour.Application.UseCases.V1.Destinations;
public class CreateOrUpdateDestinationValidator<T> : AbstractValidator<T> where T : CreateOrUpdateDestinationCommand
{
    public CreateOrUpdateDestinationValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Type)
            .Must(value => Enum.IsDefined(typeof(DestinationType), value))
            .WithMessage("Invalid destination type.");

        RuleFor(x => x.ImageUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("ImageUrl must be a valid URL.");

        RuleFor(x => x.ParentId)
            .Must(guid => guid.HasValue && guid != Guid.Empty)
            .When(x => x.ParentId.HasValue)
            .WithMessage("ParentId must be a valid Guid.");
    }
}
