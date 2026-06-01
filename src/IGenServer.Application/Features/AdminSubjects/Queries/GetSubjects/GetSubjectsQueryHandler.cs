using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjects.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjects.Queries.GetSubjects;

public sealed class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IReadOnlyList<SubjectDto>>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectsQueryHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<IReadOnlyList<SubjectDto>> Handle(GetSubjectsQuery query, CancellationToken cancellationToken)
    {
        var subjects = await _subjectRepository.GetByProgramIdAsync(query.ProgramId, cancellationToken);

        return subjects
            .Select(subject => new SubjectDto(
                subject.Id,
                subject.ProgramId,
                subject.Name,
                subject.Description,
                subject.DurationMonths,
                subject.IsPublished))
            .ToList();
    }
}