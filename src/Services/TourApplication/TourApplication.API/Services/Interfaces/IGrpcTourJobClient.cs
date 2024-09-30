using TourApplication.API.Models;

namespace TourApplication.API.Services.Interfaces;

public interface IGrpcTourJobClient
{
    TourJob GetTourJob(string id);
}
