using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistence.Repositories;

public sealed class StudentCourseRepository : IStudentCourseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentCourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Course>> GetAvailableCoursesAsync(
        int studentUserId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .Include(course => course.CourseSubjects)
            .Include(course => course.Enrollments.Where(enrollment => enrollment.StudentUserId == studentUserId))
            .Where(course => course.Status != "Archived")
            .OrderBy(course => course.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<StudentCourseEnrollment>> GetEnrollmentsAsync(
        int studentUserId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.StudentCourseEnrollments
            .AsNoTracking()
            .Include(enrollment => enrollment.Course)
            .ThenInclude(course => course.CourseSubjects)
            .Where(enrollment => enrollment.StudentUserId == studentUserId)
            .OrderByDescending(enrollment => enrollment.EnrolledAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetCourseDetailsAsync(
        int courseId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .Include(course => course.CourseSubjects)
            .ThenInclude(courseSubject => courseSubject.Subject)
            .FirstOrDefaultAsync(course => course.Id == courseId, cancellationToken);
    }

    public Task<bool> IsEnrolledAsync(
        int studentUserId,
        int courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.StudentCourseEnrollments.AnyAsync(
            enrollment => enrollment.StudentUserId == studentUserId && enrollment.CourseId == courseId,
            cancellationToken);
    }

    public async Task AddEnrollmentAsync(
        StudentCourseEnrollment enrollment,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.StudentCourseEnrollments.AddAsync(enrollment, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}