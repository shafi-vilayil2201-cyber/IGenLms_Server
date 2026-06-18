namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record CreateCourseRequestDto(
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price);