using BuildingBlocks.Infrastructure.Common;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Infrastructure.Persistence;

namespace Tour.Infrastructure.Repositories;
public class TourDetailDestinationRepository : RepositoryBase<TourDetailDestination, Guid, TourDbContext>, ITourDetailDestinationRepository
{
    public TourDetailDestinationRepository(TourDbContext context) : base(context)
    {
    }
}
