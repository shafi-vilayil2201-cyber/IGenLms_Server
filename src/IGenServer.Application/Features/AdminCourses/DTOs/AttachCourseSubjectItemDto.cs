namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record AttachCourseSubjectItemDto(
    int SubjectId,
    int DisplayOrder,
    int StartMonth);