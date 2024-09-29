using Grpc.Core;
using Serilog;
using Tour.Application.Interfaces;

namespace Tour.Application.Services;
public class GrpcTourJobService : GrpcTourJob.GrpcTourJobBase
{
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public GrpcTourJobService(ILogger logger, ITourJobRepository tourJobRepository)
    {
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public override async Task<GrpcTourJobResponse> GetTourJob(GetTourJobRequest request, ServerCallContext context)
    {
        _logger.Information("===> Received GRPC request for tourjob");

        var tourJob = await _tourJobRepository.FindSingleAsync(x => x.Id == Guid.Parse(request.Id));

        if (tourJob == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
        }

        var response = new GrpcTourJobResponse
        {
            Tourjob = new GrpcTourJobModel
            {
                Id = tourJob.Id.ToString(),
                ExpiredDate = tourJob.ExpiredDate.DateTime.ToString(),
                Owner = tourJob.CreatedBy,
            }
        };

        return response;
    }
}
