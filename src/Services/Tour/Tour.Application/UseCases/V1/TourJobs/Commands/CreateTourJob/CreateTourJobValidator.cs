using Tour.Application.Interfaces;

namespace Tour.Application.UseCases.V1.TourJobs;
public class CreateTourJobValidator : CreateOrUpdateValidator<CreateTourJobCommand>
{
    public CreateTourJobValidator(IDestinationRepository destinationRepository) : base(destinationRepository)
    {
    }
}
