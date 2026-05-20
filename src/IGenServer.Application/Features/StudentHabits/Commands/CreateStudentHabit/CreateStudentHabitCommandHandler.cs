using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentHabits.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.StudentHabits.Commands.CreateStudentHabit;

public sealed class CreateStudentHabitCommandHandler
    : IRequestHandler<CreateStudentHabitCommand, StudentHabitDto>
{
    private readonly IStudentHabitRepository _studentHabitRepository;

    public CreateStudentHabitCommandHandler(IStudentHabitRepository studentHabitRepository)
    {
        _studentHabitRepository = studentHabitRepository;
    }

    public async Task<StudentHabitDto> Handle(
        CreateStudentHabitCommand command,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var habit = new StudentHabit
        {
            UserId = command.UserId,
            Title = command.Title,
            Description = command.Description,
            HabitType = command.HabitType,
            ReminderTime = command.ReminderTime,
            IsReminderEnabled = command.IsReminderEnabled,
            IsActive = true,
            TargetMinutes = command.TargetMinutes,
            Points = command.Points,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        await _studentHabitRepository.AddAsync(habit, cancellationToken);
        await _studentHabitRepository.SaveChangesAsync(cancellationToken);

        return new StudentHabitDto
        {
            Id = habit.Id,
            Title = habit.Title,
            Description = habit.Description,
            HabitType = habit.HabitType.ToString(),
            ReminderTime = habit.ReminderTime,
            IsReminderEnabled = habit.IsReminderEnabled,
            IsActive = habit.IsActive,
            TargetMinutes = habit.TargetMinutes,
            Points = habit.Points,
            IsCompletedToday = false,
            CompletedMinutesToday = 0
        };
    }
}