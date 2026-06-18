using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetStudentCourseDetails;

public sealed record GetStudentCourseDetailsQuery(
    int StudentUserId,
    int CourseId) : IRequest<StudentCourseDetailsDto>;