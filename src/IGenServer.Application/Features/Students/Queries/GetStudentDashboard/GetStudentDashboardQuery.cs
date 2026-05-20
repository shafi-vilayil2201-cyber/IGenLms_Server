

using IGenServer.Application.Features.Students.DTOs;
using MediatR;

namespace IGenServer.Application.Features.Students.Queries.GetStudentDashboard;

public sealed record GetStudentDashboardQuery(
    int UserId) : IRequest<StudentDashboardResponseDto>;

