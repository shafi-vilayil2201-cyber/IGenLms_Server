namespace IGenServer.Application.Features.StudentCourses.DTOs;

public sealed record StudentCourseDto(
    int Id,
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price,
    string Status,
    int SubjectCount,
    bool IsEnrolled);
    