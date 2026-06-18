using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetMyCourses;

public sealed record GetMyCoursesQuery(int StudentUserId) : IRequest<IReadOnlyList<StudentCourseDto>>;