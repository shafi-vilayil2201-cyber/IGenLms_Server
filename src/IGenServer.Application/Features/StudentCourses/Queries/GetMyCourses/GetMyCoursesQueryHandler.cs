using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetMyCourses;

public sealed class GetMyCoursesQueryHandler
    : IRequestHandler<GetMyCoursesQuery, IReadOnlyList<StudentCourseDto>>
{
    private readonly IStudentCourseRepository _repository;

    public GetMyCoursesQueryHandler(IStudentCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentCourseDto>> Handle(
        GetMyCoursesQuery query,
        CancellationToken cancellationToken)
    {
        var enrollments = await _repository.GetEnrollmentsAsync(query.StudentUserId, cancellationToken);

        return enrollments
            .Select(enrollment => StudentCourseMapper.ToListDto(enrollment.Course, true))
            .ToList();
    }
}