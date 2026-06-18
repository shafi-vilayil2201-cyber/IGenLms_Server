using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetAvailableCourses;

public sealed record GetAvailableCoursesQuery(int StudentUserId) : IRequest<IReadOnlyList<StudentCourseDto>>;