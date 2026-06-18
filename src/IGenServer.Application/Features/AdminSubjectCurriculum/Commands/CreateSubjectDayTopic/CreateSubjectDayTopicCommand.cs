using IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminSubjectCurriculum.Commands.CreateSubjectDayTopic;

public sealed record CreateSubjectDayTopicCommand(
    int SubjectWeekId,
    int DayNumber,
    string Title,
    string Description,
    int EstimatedMinutes) : IRequest<SubjectDayTopicDto>;