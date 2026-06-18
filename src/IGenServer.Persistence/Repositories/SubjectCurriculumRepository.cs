using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistence.Repositories;

public sealed class SubjectCurriculumRepository : ISubjectCurriculumRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SubjectCurriculumRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> SubjectExistsAsync(int subjectId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Subjects.AnyAsync(x => x.Id == subjectId, cancellationToken);
    }

    public Task<bool> SubjectMonthExistsAsync(int subjectId, int monthNumber, CancellationToken cancellationToken = default)
    {
        return _dbContext.SubjectMonths.AnyAsync(
            x => x.SubjectId == subjectId && x.MonthNumber == monthNumber,
            cancellationToken);
    }

    public Task<bool> SubjectMonthIdExistsAsync(int subjectMonthId, CancellationToken cancellationToken = default)
    {
        return _dbContext.SubjectMonths.AnyAsync(x => x.Id == subjectMonthId, cancellationToken);
    }

    public Task<bool> SubjectWeekExistsAsync(int subjectMonthId, int weekNumber, CancellationToken cancellationToken = default)
    {
        return _dbContext.SubjectWeeks.AnyAsync(
            x => x.SubjectMonthId == subjectMonthId && x.WeekNumber == weekNumber,
            cancellationToken);
    }

    public Task<bool> SubjectWeekIdExistsAsync(int subjectWeekId, CancellationToken cancellationToken = default)
    {
        return _dbContext.SubjectWeeks.AnyAsync(x => x.Id == subjectWeekId, cancellationToken);
    }

    public Task<bool> SubjectDayTopicExistsAsync(int subjectWeekId, int dayNumber, CancellationToken cancellationToken = default)
    {
        return _dbContext.SubjectDayTopics.AnyAsync(
            x => x.SubjectWeekId == subjectWeekId && x.DayNumber == dayNumber,
            cancellationToken);
    }

    public async Task AddMonthAsync(SubjectMonth month, CancellationToken cancellationToken = default)
    {
        await _dbContext.SubjectMonths.AddAsync(month, cancellationToken);
    }

    public async Task AddWeekAsync(SubjectWeek week, CancellationToken cancellationToken = default)
    {
        await _dbContext.SubjectWeeks.AddAsync(week, cancellationToken);
    }

    public async Task AddDayTopicAsync(SubjectDayTopic dayTopic, CancellationToken cancellationToken = default)
    {
        await _dbContext.SubjectDayTopics.AddAsync(dayTopic, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}