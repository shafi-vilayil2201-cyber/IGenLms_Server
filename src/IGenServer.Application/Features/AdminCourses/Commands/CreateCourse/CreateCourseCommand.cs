using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
    int ProgramId,
    string Title,
    string Description,
    int DurationMonths,
    decimal Price) : IRequest<CourseDto>;