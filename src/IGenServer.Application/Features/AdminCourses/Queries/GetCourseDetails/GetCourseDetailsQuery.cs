using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Queries.GetCourseDetails;

public sealed record GetCourseDetailsQuery(int CourseId) : IRequest<CourseDetailsDto>;