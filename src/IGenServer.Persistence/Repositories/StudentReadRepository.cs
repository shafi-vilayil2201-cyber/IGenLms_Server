using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Students.DTOs;
using IGenServer.Domain.Entities;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Persistance.Repositories;

public sealed class StudentReadRepository : IStudentReadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentReadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentDashboardResponseDto?> GetDashboardAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .Select(user => new StudentDashboardResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                TargetYear = user.StudentProfile != null ? user.StudentProfile.TargetYear : 0,
                StudyStreak = user.StudentProfile != null ? user.StudentProfile.StudyStreak : 0,
                Rank = user.StudentProfile != null ? user.StudentProfile.Rank : 0,
                EnrolledCoursesCount = _dbContext.StudentCourseEnrollments
                    .Count(enrollment => enrollment.StudentUserId == user.Id)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
