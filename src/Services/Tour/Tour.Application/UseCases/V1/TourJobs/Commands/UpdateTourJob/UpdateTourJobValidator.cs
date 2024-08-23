using Tour.Application.Interfaces;

namespace Tour.Application.UseCases.V1.TourJobs;
public class UpdateTourJobValidator : CreateOrUpdateValidator<UpdateTourJobCommand>
{
    public UpdateTourJobValidator(IDestinationRepository destinationRepository) : base(destinationRepository)
    {
    }
}
