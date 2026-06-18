namespace IGenServer.Application.Features.AdminSubjectCurriculum.DTOs;

public sealed record SubjectCurriculumWeekDto(
    int Id,
    int WeekNumber,
    string Title,
    IReadOnlyList<SubjectCurriculumDayTopicDto> DayTopics);