using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Queries.GetSubjectCurriculum;

public sealed class GetSubjectCurriculumQueryHandler
    : IRequestHandler<GetSubjectCurriculumQuery, SubjectCurriculumDto>
{
    private readonly IAdminCurriculumReadRepository _repository;

    public GetSubjectCurriculumQueryHandler(IAdminCurriculumReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubjectCurriculumDto> Handle(
        GetSubjectCurriculumQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetSubjectCurriculumAsync(
            query.SubjectId,
            cancellationToken);

        if (result is null)
        {
            throw new InvalidOperationException("Subject curriculum does not exist.");
        }

        return result;
    }
}