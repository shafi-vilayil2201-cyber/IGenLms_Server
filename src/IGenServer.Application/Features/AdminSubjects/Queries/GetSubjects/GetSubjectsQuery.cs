using IGenServer.Application.Features.AdminSubjects.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjects.Queries.GetSubjects;

public sealed record GetSubjectsQuery(int ProgramId) : IRequest<IReadOnlyList<SubjectDto>>;