using System.Data;
using Dapper;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IGenServer.Persistence.Repositories.Dapper;

public sealed class AdminCurriculumReadRepository : IAdminCurriculumReadRepository
{
    private readonly string _connectionString;

    public AdminCurriculumReadRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured.");
    }

    public async Task<SubjectCurriculumDto?> GetSubjectCurriculumAsync(
        int subjectId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                s.Id AS SubjectId,
                s.Name AS SubjectName,
                s.ProgramId AS ProgramId,

                sm.Id AS MonthId,
                sm.MonthNumber AS MonthNumber,
                sm.Title AS MonthTitle,

                sw.Id AS WeekId,
                sw.WeekNumber AS WeekNumber,
                sw.Title AS WeekTitle,

                sdt.Id AS DayTopicId,
                sdt.DayNumber AS DayNumber,
                sdt.Title AS DayTopicTitle,
                sdt.Description AS DayTopicDescription,
                sdt.EstimatedMinutes AS EstimatedMinutes
            FROM Subjects s
            LEFT JOIN SubjectMonths sm ON sm.SubjectId = s.Id
            LEFT JOIN SubjectWeeks sw ON sw.SubjectMonthId = sm.Id
            LEFT JOIN SubjectDayTopics sdt ON sdt.SubjectWeekId = sw.Id
            WHERE s.Id = @SubjectId
            ORDER BY
                sm.MonthNumber,
                sw.WeekNumber,
                sdt.DayNumber;
            """;

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = new CommandDefinition(
            sql,
            new { SubjectId = subjectId },
            commandType: CommandType.Text,
            cancellationToken: cancellationToken);

        var rows = (await connection.QueryAsync<SubjectCurriculumRowDto>(command)).ToList();

        if (rows.Count == 0)
        {
            return null;
        }

        var first = rows[0];

        var months = rows
            .Where(row => row.MonthId.HasValue)
            .GroupBy(row => new
            {
                Id = row.MonthId!.Value,
                Number = row.MonthNumber!.Value,
                Title = row.MonthTitle ?? string.Empty
            })
            .OrderBy(group => group.Key.Number)
            .Select(monthGroup =>
            {
                var weeks = monthGroup
                    .Where(row => row.WeekId.HasValue)
                    .GroupBy(row => new
                    {
                        Id = row.WeekId!.Value,
                        Number = row.WeekNumber!.Value,
                        Title = row.WeekTitle ?? string.Empty
                    })
                    .OrderBy(group => group.Key.Number)
                    .Select(weekGroup =>
                    {
                        var dayTopics = weekGroup
                            .Where(row => row.DayTopicId.HasValue)
                            .OrderBy(row => row.DayNumber)
                            .Select(row => new SubjectCurriculumDayTopicDto(
                                row.DayTopicId!.Value,
                                row.DayNumber!.Value,
                                row.DayTopicTitle ?? string.Empty,
                                row.DayTopicDescription ?? string.Empty,
                                row.EstimatedMinutes ?? 0))
                            .ToList();

                        return new SubjectCurriculumWeekDto(
                            weekGroup.Key.Id,
                            weekGroup.Key.Number,
                            weekGroup.Key.Title,
                            dayTopics);
                    })
                    .ToList();

                return new SubjectCurriculumMonthDto(
                    monthGroup.Key.Id,
                    monthGroup.Key.Number,
                    monthGroup.Key.Title,
                    weeks);
            })
            .ToList();

        return new SubjectCurriculumDto(
            first.SubjectId,
            first.SubjectName,
            first.ProgramId,
            months);
    }
}
