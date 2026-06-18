using FluentValidation;

namespace IGenServer.Application.Features.AdminCourses.Commands.CreateCourse;

public sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.ProgramId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.DurationMonths).GreaterThan(0).LessThanOrEqualTo(36);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}