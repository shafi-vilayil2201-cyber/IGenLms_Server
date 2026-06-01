using FluentValidation;

namespace IGenServer.Application.Features.AdminCourses.Commands.AttachCourseSubjects;

public sealed class AttachCourseSubjectsCommandValidator : AbstractValidator<AttachCourseSubjectsCommand>
{
    public AttachCourseSubjectsCommandValidator()
    {
        RuleFor(x => x.CourseId).GreaterThan(0);

        RuleFor(x => x.Subjects)
            .NotEmpty();

        RuleForEach(x => x.Subjects).ChildRules(subject =>
        {
            subject.RuleFor(x => x.SubjectId).GreaterThan(0);
            subject.RuleFor(x => x.DisplayOrder).GreaterThan(0);
            subject.RuleFor(x => x.StartMonth).GreaterThan(0);
        });
    }
}