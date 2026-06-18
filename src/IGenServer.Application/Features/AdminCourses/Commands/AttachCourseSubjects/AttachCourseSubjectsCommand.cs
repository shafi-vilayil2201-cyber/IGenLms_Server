using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Commands.AttachCourseSubjects;

public sealed record AttachCourseSubjectsCommand(
    int CourseId,
    IReadOnlyList<AttachCourseSubjectItemDto> Subjects) : IRequest<CourseDetailsDto>;