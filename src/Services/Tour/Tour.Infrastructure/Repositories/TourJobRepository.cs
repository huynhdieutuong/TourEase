using BuildingBlocks.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Infrastructure.Persistence;

namespace Tour.Infrastructure.Repositories;
public class TourJobRepository : RepositoryBase<TourJob, Guid, TourDbContext>, ITourJobRepository
{
    public TourJobRepository(TourDbContext context) : base(context)
    {
    }

    public async Task<TourJob> GetTourJobByIdAsync(Guid id)
    {
        return await _context.TourJob
                        .Include(t => t.Detail)
                            .ThenInclude(d => d.TourDetailDestinations)
                                .ThenInclude(td => td.Destination)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(t => t.Id == id);
    }
}
