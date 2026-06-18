using IGenServer.Application.Features.StudentHabits.DTOs;
using IGenServer.Domain.Entities;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IStudentHabitRepository
{
    Task AddAsync(StudentHabit habit, CancellationToken cancellationToken);
    Task<IReadOnlyList<StudentHabitDto>> GetTodayHabitsAsync(
        int userId,
        DateOnly date,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}