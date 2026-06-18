using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Commands.EnrollInCourse;

public sealed record EnrollInCourseCommand(
    int StudentUserId,
    int CourseId) : IRequest<StudentCourseDetailsDto>;