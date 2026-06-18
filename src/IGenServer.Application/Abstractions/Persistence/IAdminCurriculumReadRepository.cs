using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

namespace IGenServer.Application.Abstractions.Persistence;

public interface IAdminCurriculumReadRepository
{
    Task<SubjectCurriculumDto?> GetSubjectCurriculumAsync(
        int subjectId,
        CancellationToken cancellationToken = default);
}