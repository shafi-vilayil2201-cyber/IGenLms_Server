namespace IGenServer.Application.Features.StudentCourses.DTOs;

public sealed record StudentCourseDetailsDto(
    int Id,
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price,
    string Status,
    bool IsEnrolled,
    IReadOnlyList<StudentCourseSubjectDto> Subjects);