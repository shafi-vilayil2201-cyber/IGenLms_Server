using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistence.Repositories;

public sealed class AdminCourseRepository : IAdminCourseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AdminCourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ProgramExistsAsync(int programId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Programs.AnyAsync(x => x.Id == programId, cancellationToken);
    }

    public Task<bool> CourseExistsAsync(int programId, string title, CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses.AnyAsync(
            x => x.ProgramId == programId && x.Title == title,
            cancellationToken);
    }

    public Task<bool> CourseIdExistsAsync(int courseId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses.AnyAsync(x => x.Id == courseId, cancellationToken);
    }

    public Task<bool> SubjectExistsInProgramAsync(
        int subjectId,
        int programId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Subjects.AnyAsync(
            x => x.Id == subjectId && x.ProgramId == programId,
            cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(int courseId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses
            .Include(x => x.CourseSubjects)
            .ThenInclude(x => x.Subject)
            .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
    }

    public async Task<IReadOnlyList<Course>> GetByProgramIdAsync(
        int programId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses
            .Where(x => x.ProgramId == programId)
            .Include(x => x.CourseSubjects)
            .ThenInclude(x => x.Subject)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        await _dbContext.Courses.AddAsync(course, cancellationToken);
    }

    public async Task AddCourseSubjectAsync(
        CourseSubject courseSubject,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.CourseSubjects.AddAsync(courseSubject, cancellationToken);
    }

    public Task<bool> CourseSubjectExistsAsync(
        int courseId,
        int subjectId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseSubjects.AnyAsync(
            x => x.CourseId == courseId && x.SubjectId == subjectId,
            cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}