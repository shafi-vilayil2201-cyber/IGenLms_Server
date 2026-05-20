using IGenServer.Application.Features.StudentHabits.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentHabits.Queries.GetTodayStudentHabits;

public sealed record GetTodayStudentHabitsQuery(
    int UserId,
    DateOnly Date
) : IRequest<IReadOnlyList<StudentHabitDto>>;