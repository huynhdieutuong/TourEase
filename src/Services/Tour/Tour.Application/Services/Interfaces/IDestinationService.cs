using Tour.Domain.Entities;

namespace Tour.Application.Services.Interfaces;
public interface IDestinationService
{
    List<Destination> BuildTree(List<Destination> destinations, Guid? parrentId = null);
}
