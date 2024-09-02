using BuildingBlocks.Contracts.Common;
using TourSearch.API.Entities;

namespace TourSearch.API.Repositories.Interfaces;

public interface IDestinationRepository : IMongoRepositoryBase<Destination, Guid>
{
}
