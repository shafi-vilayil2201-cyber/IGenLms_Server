namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record CourseDetailsDto(
    int Id,
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price,
    string Status,
    IReadOnlyList<CourseSubjectDto> Subjects);