namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record CourseDto(
    int Id,
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price,
    string Status,
    int SubjectCount);