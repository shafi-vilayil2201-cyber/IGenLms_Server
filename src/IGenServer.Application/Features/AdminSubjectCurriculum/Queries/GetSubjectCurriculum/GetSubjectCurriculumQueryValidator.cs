using FluentValidation;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Queries.GetSubjectCurriculum;

public sealed class GetSubjectCurriculumQueryValidator : AbstractValidator<GetSubjectCurriculumQuery>
{
    public GetSubjectCurriculumQueryValidator()
    {
        RuleFor(x => x.SubjectId)
            .GreaterThan(0);
    }
}