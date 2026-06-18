using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectMonth;

public sealed record CreateSubjectMonthCommand(
    int SubjectId,
    int MonthNumber,
    string Title) : IRequest<SubjectMonthDto>;