using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Students.DTOs;
using MediatR;

namespace IGenServer.Application.Features.Students.Queries.GetStudentDashboard;

public sealed class GetStudentDashboardQueryHandler
    : IRequestHandler<GetStudentDashboardQuery, StudentDashboardResponseDto>
{
    private readonly IStudentReadRepository _studentReadRepository;

    public GetStudentDashboardQueryHandler(
        IStudentReadRepository studentReadRepository)
    {
        _studentReadRepository = studentReadRepository;
    }

    public async Task<StudentDashboardResponseDto> Handle(
        GetStudentDashboardQuery request,
        CancellationToken cancellationToken)
    {
        var dashboard = await _studentReadRepository.GetDashboardAsync(
            request.UserId,
            cancellationToken);

        if (dashboard is null)
        {
            throw new UnauthorizedAccessException("Student dashboard is not available.");
        }

        return dashboard;
    }
}