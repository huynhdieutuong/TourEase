using BuildingBlocks.Infrastructure.Common;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Persistence;

namespace Tour.Infrastructure.Repositories;
public class TourUnitOfWork : UnitOfWork<TourDbContext>, ITourUnitOfWork
{
    public TourUnitOfWork(TourDbContext context) : base(context)
    {
    }
}
