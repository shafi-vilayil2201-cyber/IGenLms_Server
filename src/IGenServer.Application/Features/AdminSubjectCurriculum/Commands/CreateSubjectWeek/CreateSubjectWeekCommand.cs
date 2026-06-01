using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectWeek;

public sealed record CreateSubjectWeekCommand(
    int SubjectMonthId,
    int WeekNumber,
    string Title) : IRequest<SubjectWeekDto>;