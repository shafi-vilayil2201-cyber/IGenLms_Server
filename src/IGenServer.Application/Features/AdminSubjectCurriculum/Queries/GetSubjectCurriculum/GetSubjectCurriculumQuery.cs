using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Queries.GetSubjectCurriculum;

public sealed record GetSubjectCurriculumQuery(
    int SubjectId) : IRequest<SubjectCurriculumDto>;