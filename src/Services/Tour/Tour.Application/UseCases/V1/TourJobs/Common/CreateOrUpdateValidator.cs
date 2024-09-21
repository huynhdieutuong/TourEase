using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tour.Application.Interfaces;
using Tour.Domain.Entities.Enums;

namespace Tour.Application.UseCases.V1.TourJobs;
public class CreateOrUpdateValidator<T> : AbstractValidator<T> where T : CreateOrUpdateCommand
{
    private readonly IDestinationRepository _destinationRepository;

    public CreateOrUpdateValidator(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository;

        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(5, 255).WithMessage("Title must be between 5 and 255 characters.");

        RuleFor(x => x.SalaryPerDay)
            .GreaterThan(0).WithMessage("Salary per day must be greater than zero.")
            .Must((command, salary) => command.Currency != (int)Currency.VND || salary == Math.Truncate(salary))
            .WithMessage("For VND, salary must not have decimal places.");

        RuleFor(x => x.Currency)
            .Must(value => Enum.IsDefined(typeof(Currency), value))
            .WithMessage("Invalid currency type.");

        RuleFor(x => x.ExpiredDate)
            .NotEmpty().WithMessage("Expired date cannot be empty.")
            .GreaterThan(DateTimeOffset.Now).WithMessage("Expired date must be in the future.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date cannot be empty.")
            .GreaterThan(x => x.ExpiredDate).WithMessage("Start date must be after the expired date.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date cannot be empty.")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be before the start date.");

        RuleFor(x => x.Itinerary)
            .NotEmpty().WithMessage("Itinerary is required.");

        RuleFor(x => x.ImageUrl)
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("Image URL must be a valid URL.");

        RuleFor(x => x.Participants)
            .GreaterThanOrEqualTo(10).WithMessage("Participants must be at least 10.");

        RuleFor(x => x.LanguageSpoken)
            .Must(value => Enum.IsDefined(typeof(Language), value))
            .WithMessage("Invalid language spoken type.");

        RuleFor(x => x.DestinationIds)
            .MustAsync(async (ids, cancellation) => await AllDestinationsExistAsync(ids))
            .WithMessage("One or more destination IDs are invalid.");
    }

    private async Task<bool> AllDestinationsExistAsync(List<Guid> destinationIds)
    {
        var existingIds = await _destinationRepository
                                    .FindAll(d => destinationIds.Contains(d.Id))
                                    .Select(d => d.Id)
                                    .ToListAsync();

        return existingIds.Count == destinationIds.Count;
    }
}
