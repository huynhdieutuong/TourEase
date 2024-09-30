using FluentValidation;

namespace TourApplication.API.UseCases.V1;

public class ApplyTourJobValidator : AbstractValidator<ApplyTourJobCommand>
{
    public ApplyTourJobValidator()
    {
        RuleFor(x => x.TourJobId)
            .NotEmpty().WithMessage("TourJobId is requried.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
    }
}
