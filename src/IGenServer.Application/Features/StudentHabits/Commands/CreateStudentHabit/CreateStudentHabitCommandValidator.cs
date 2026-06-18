using FluentValidation;

namespace IGenServer.Application.Features.StudentHabits.Commands.CreateStudentHabit;

public sealed class CreateStudentHabitCommandValidator : AbstractValidator<CreateStudentHabitCommand>
{
    public CreateStudentHabitCommandValidator()
    {
        RuleFor(command => command.UserId)
            .GreaterThan(0);

        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(command => command.Description)
            .MaximumLength(500);

        RuleFor(command => command.ReminderTime)
            .NotEmpty()
            .Matches(@"^([01]\d|2[0-3]):[0-5]\d$")
            .WithMessage("Reminder time must be in HH:mm format.");

        RuleFor(command => command.TargetMinutes)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1440);

        RuleFor(command => command.Points)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}