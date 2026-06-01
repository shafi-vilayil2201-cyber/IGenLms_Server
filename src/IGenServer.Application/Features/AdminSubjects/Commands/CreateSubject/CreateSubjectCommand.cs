
using IGenServer.Application.Features.AdminSubjects.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjects.Commands.CreateSubject;

public sealed record CreateSubjectCommand(
    int ProgramId,
    string Name,
    string Description,
    int DurationMonths
) : IRequest<SubjectDto>;