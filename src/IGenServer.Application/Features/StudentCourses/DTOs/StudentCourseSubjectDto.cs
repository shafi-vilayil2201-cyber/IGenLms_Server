namespace IGenServer.Application.Features.StudentCourses.DTOs;

public sealed record StudentCourseSubjectDto(
    int SubjectId,
    string SubjectName,
    int DisplayOrder,
    int StartMonth);