﻿using BuildingBlocks.Contracts.Common;
using Tour.Domain.Entities;

namespace Tour.Application.Interfaces;
public interface ITourJobRepository : IRepositoryBase<TourJob, Guid>
{
    Task<TourJob> GetTourJobByIdAsync(Guid id);
}
