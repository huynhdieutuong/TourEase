﻿using BuildingBlocks.Shared.Exceptions;
using Dapper;
using MassTransit;
using TourApplication.API.Models;
using TourApplication.API.Persistence.Interfaces;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.UseCases.V1;

namespace TourApplication.API.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ApplicationRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Application?> GetApplicationByIdAsync(Guid id)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"SELECT * FROM Application
                    WHERE Id = @Id";

        var result = await connection.QuerySingleOrDefaultAsync<Application>(
            sql,
            new { Id = id });

        return result;
    }

    public async Task<Application?> GetApplicationByTourJobIdAndUsernameAsync(Guid tourJobId, string username)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"SELECT * FROM Application
                    WHERE TourJobId = @TourJobId AND TourGuide = @TourGuide";

        var result = await connection.QuerySingleOrDefaultAsync<Application>(
            sql,
            new { TourJobId = tourJobId, TourGuide = username });

        return result;
    }

    public async Task<List<Application>> GetApplicationsByTourJobIdAsync(Guid tourJobId)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"SELECT * FROM Application 
                    WHERE TourJobId = @TourJobId AND Status <> @CanceledStatus";

        var result = await connection.QueryAsync<Application>(
            sql,
            new { TourJobId = tourJobId, CanceledStatus = Status.Canceled });

        return result.ToList();
    }

    public async Task<List<Application>> GetMyApplications(string username)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"SELECT * FROM Application
                    WHERE TourGuide = @TourGuide";

        var result = await connection.QueryAsync<Application>(sql, new { TourGuide = username });

        return result.ToList();
    }

    public async Task<Guid> CreateApplicationAsync(ApplyTourJobCommand command)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"INSERT INTO Application (Id, TourJobId, TourGuide, Comment) 
                    VALUES (@Id, @TourJobId, @TourGuide, @Comment)";

        var newId = Guid.NewGuid();
        var result = await connection.ExecuteAsync(
            sql,
            new { Id = newId, command.TourJobId, TourGuide = command.Username, command.Comment });

        return newId;
    }

    public async Task<bool> CancelApplicationAsync(Guid applicationId)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"
            UPDATE Application
            SET Status = @CanceledStatus
            WHERE Id = @ApplicationId AND Status = @PendingStatus;";

        var result = await connection.ExecuteAsync(
            sql,
            new
            {
                ApplicationId = applicationId,
                CanceledStatus = Status.Canceled,
                PendingStatus = Status.Pending
            });

        return result > 0;
    }

    public async Task<bool> ReApplyApplicationAsync(Guid applicationId)
    {
        using var connection = _dbConnectionFactory.Create();

        var sql = @"
            UPDATE Application
            SET Status = @PendingStatus, AppliedDate = @AppliedDate
            WHERE Id = @ApplicationId AND Status = @CanceledStatus;";

        var result = await connection.ExecuteAsync(
            sql,
            new
            {
                ApplicationId = applicationId,
                AppliedDate = DateTime.UtcNow,
                CanceledStatus = Status.Canceled,
                PendingStatus = Status.Pending
            });

        return result > 0;
    }

    public async Task ChooseTourGuideAsync(Guid applicationId, Guid tourJobId)
    {
        using var connection = _dbConnectionFactory.Create();

        using var transaction = connection.BeginTransaction();

        try
        {
            var sqlAccept = @"
                UPDATE Application
                SET Status = @AcceptedStatus
                WHERE Id = @ApplicationId AND Status = @PendingStatus";

            var acceptedRows = await connection.ExecuteAsync(
                sqlAccept,
                new
                {
                    ApplicationId = applicationId,
                    AcceptedStatus = Status.Accepted,
                    PendingStatus = Status.Pending
                },
                transaction);

            if (acceptedRows == 0)
            {
                throw new BadRequestException("No application was accepted. The application may have already been processed or does not exist.");
            }

            var sqlRejectOthers = @"
                UPDATE Application
                SET Status = @RejectedStatus
                WHERE TourJobId = @TourJobId AND Status = @PendingStatus AND Id <> @ApplicationId";

            await connection.ExecuteAsync(
                sqlRejectOthers,
                new
                {
                    ApplicationId = applicationId,
                    TourJobId = tourJobId,
                    RejectedStatus = Status.Rejected,
                    PendingStatus = Status.Pending
                },
                transaction);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}
