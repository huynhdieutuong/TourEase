using MediatR;

namespace Tour.Application.UseCases.V1.Destinations;
public record DeleteDestinationCommand(Guid Id) : IRequest
{
}
