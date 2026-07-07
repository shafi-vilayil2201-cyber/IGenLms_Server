using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IStudentCourseRepository
{
    Task<IReadOnlyList<Course>> GetAvailableCoursesAsync(int studentUserId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudentCourseEnrollment>> GetEnrollmentsAsync(int studentUserId, CancellationToken cancellationToken = default);
    Task<Course?> GetCourseDetailsAsync(int courseId, CancellationToken cancellationToken = default);
    Task<bool> IsEnrolledAsync(int studentUserId, int courseId, CancellationToken cancellationToken = default);
    Task AddEnrollmentAsync(StudentCourseEnrollment enrollment, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
