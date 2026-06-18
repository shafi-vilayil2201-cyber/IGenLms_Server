using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Queries.GetCourses;

public sealed record GetCoursesQuery(int ProgramId) : IRequest<IReadOnlyList<CourseDto>>;