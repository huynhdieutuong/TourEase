using BuildingBlocks.Infrastructure.Common;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Infrastructure.Persistence;

namespace Tour.Infrastructure.Repositories;
public class DestinationRepository : RepositoryBase<Destination, Guid, TourDbContext>, IDestinationRepository
{
    public DestinationRepository(TourDbContext context) : base(context)
    {
    }
}
