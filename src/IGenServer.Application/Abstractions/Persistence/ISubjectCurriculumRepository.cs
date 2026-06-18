using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface ISubjectCurriculumRepository
{
    Task<bool> SubjectExistsAsync(int subjectId, CancellationToken cancellationToken = default);
    Task<bool> SubjectMonthExistsAsync(int subjectId, int monthNumber, CancellationToken cancellationToken = default);
    Task<bool> SubjectMonthIdExistsAsync(int subjectMonthId, CancellationToken cancellationToken = default);
    Task<bool> SubjectWeekExistsAsync(int subjectMonthId, int weekNumber, CancellationToken cancellationToken = default);
    Task<bool> SubjectWeekIdExistsAsync(int subjectWeekId, CancellationToken cancellationToken = default);
    Task<bool> SubjectDayTopicExistsAsync(int subjectWeekId, int dayNumber, CancellationToken cancellationToken = default);

    Task AddMonthAsync(SubjectMonth month, CancellationToken cancellationToken = default);
    Task AddWeekAsync(SubjectWeek week, CancellationToken cancellationToken = default);
    Task AddDayTopicAsync(SubjectDayTopic dayTopic, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}