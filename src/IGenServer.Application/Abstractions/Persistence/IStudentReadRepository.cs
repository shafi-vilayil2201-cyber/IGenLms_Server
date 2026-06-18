using IGenServer.Application.Features.Students.DTOs;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IStudentReadRepository
{
    Task<StudentDashboardResponseDto?> GetDashboardAsync(
        int userId,
        CancellationToken cancellationToken);
}