using AutoMapper;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using Serilog;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.TourJobs;
public class DeleteTourJobCommandHandler : IRequestHandler<DeleteTourJobCommand>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourDetailDestinationRepository _tourDetailDestinationRepository;

    private const string MethodName = nameof(DeleteTourJobCommandHandler);

    public DeleteTourJobCommandHandler(ILogger logger,
                                       IMapper mapper,
                                       ITourUnitOfWork tourUnitOfWork,
                                       ITourJobRepository tourJobRepository,
                                       ITourDetailDestinationRepository tourDetailDestinationRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _tourUnitOfWork = tourUnitOfWork;
        _tourJobRepository = tourJobRepository;
        _tourDetailDestinationRepository = tourDetailDestinationRepository;
    }

    public async Task Handle(DeleteTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Tour Job Id: {request.Id}");

        var tourJob = await _tourJobRepository.FindByIdAsync(request.Id);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), request.Id);

        _tourJobRepository.Remove(tourJob);
        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} - Tour Job Id: {request.Id}");
    }
}
