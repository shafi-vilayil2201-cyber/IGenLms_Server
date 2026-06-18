using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IAdminCourseRepository
{
    Task<bool> ProgramExistsAsync(int programId, CancellationToken cancellationToken = default);
    Task<bool> CourseExistsAsync(int programId, string title, CancellationToken cancellationToken = default);
    Task<bool> CourseIdExistsAsync(int courseId, CancellationToken cancellationToken = default);
    Task<bool> SubjectExistsInProgramAsync(int subjectId, int programId, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(int courseId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Course>> GetByProgramIdAsync(int programId, CancellationToken cancellationToken = default);
    Task AddAsync(Course course, CancellationToken cancellationToken = default);
    Task AddCourseSubjectAsync(CourseSubject courseSubject, CancellationToken cancellationToken = default);
    Task<bool> CourseSubjectExistsAsync(int courseId, int subjectId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}