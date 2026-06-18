using FluentValidation;

namespace IGenServer.Application.Features.StudentCourses.Commands.EnrollInCourse;

public sealed class EnrollInCourseCommandValidator : AbstractValidator<EnrollInCourseCommand>
{
    public EnrollInCourseCommandValidator()
    {
        RuleFor(x => x.StudentUserId).GreaterThan(0);
        RuleFor(x => x.CourseId).GreaterThan(0);
    }
}