namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record CourseSubjectDto(
    int SubjectId,
    string SubjectName,
    int DisplayOrder,
    int StartMonth);