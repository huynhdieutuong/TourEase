using MediatR;

namespace Tour.Application.UseCases.V1.TourJobs;
public record DeleteTourJobCommand(Guid Id) : IRequest
{
}
