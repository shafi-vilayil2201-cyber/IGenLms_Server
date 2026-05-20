using IGenServer.Application.Features.StudentHabits.DTOs;
using IGenServer.Domain.Enums;
using MediatR;

namespace IGenServer.Application.Features.StudentHabits.Commands.CreateStudentHabit;

public sealed record CreateStudentHabitCommand(
    int UserId,
    string Title,
    string? Description,
    StudentHabitType HabitType,
    string ReminderTime,
    bool IsReminderEnabled,
    int TargetMinutes,
    int Points
) : IRequest<StudentHabitDto>;