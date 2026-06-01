namespace IGenServer.Application.Features.AdminCourses.DTOs;

public sealed record AttachCourseSubjectsRequestDto(
    IReadOnlyList<AttachCourseSubjectItemDto> Subjects);