using Dapper;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentHabits.DTOs;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IGenServer.Persistence.Repositories;

public sealed class StudentHabitRepository : IStudentHabitRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly string _connectionString;

    public StudentHabitRepository(
        ApplicationDbContext dbContext,
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured.");
    }

    public async Task AddAsync(StudentHabit habit, CancellationToken cancellationToken)
    {
        await _dbContext.StudentHabits.AddAsync(habit, cancellationToken);
    }

    public async Task<IReadOnlyList<StudentHabitDto>> GetTodayHabitsAsync(
        int userId,
        DateOnly date,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                h.Id,
                h.Title,
                h.Description,
                h.HabitType,
                h.ReminderTime,
                h.IsReminderEnabled,
                h.IsActive,
                h.TargetMinutes,
                h.Points,
                CAST(CASE WHEN l.Id IS NULL THEN 0 ELSE 1 END AS bit) AS IsCompletedToday,
                COALESCE(l.CompletedMinutes, 0) AS CompletedMinutesToday
            FROM StudentHabits h
            LEFT JOIN StudentHabitLogs l
                ON l.StudentHabitId = h.Id
                AND l.UserId = @UserId
                AND l.Date = @Date
                AND l.IsCompleted = 1
            WHERE h.UserId = @UserId
                AND h.IsActive = 1
            ORDER BY h.ReminderTime;
            """;

        await using var connection = new SqlConnection(_connectionString);

        var habits = await connection.QueryAsync<StudentHabitDto>(
            new CommandDefinition(
                sql,
                new
                {
                    UserId = userId,
                    Date = date.ToDateTime(TimeOnly.MinValue)
                },
                cancellationToken: cancellationToken));

        return habits.ToList();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}