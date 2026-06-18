using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetAvailableCourses;

public sealed class GetAvailableCoursesQueryHandler
    : IRequestHandler<GetAvailableCoursesQuery, IReadOnlyList<StudentCourseDto>>
{
    private readonly IStudentCourseRepository _repository;

    public GetAvailableCoursesQueryHandler(IStudentCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentCourseDto>> Handle(
        GetAvailableCoursesQuery query,
        CancellationToken cancellationToken)
    {
        var courses = await _repository.GetAvailableCoursesAsync(query.StudentUserId, cancellationToken);

        return courses
            .Select(course => StudentCourseMapper.ToListDto(course, course.Enrollments.Any()))
            .ToList();
    }
}