using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Queries.GetCourses;

public sealed class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IReadOnlyList<CourseDto>>
{
    private readonly IAdminCourseRepository _repository;

    public GetCoursesQueryHandler(IAdminCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CourseDto>> Handle(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        var courses = await _repository.GetByProgramIdAsync(query.ProgramId, cancellationToken);

        return courses
            .Select(course => new CourseDto(
                course.Id,
                course.ProgramId,
                course.Title,
                course.Description,
                course.DurationMonths,
                course.Price,
                course.Status,
                course.CourseSubjects.Count))
            .ToList();
    }
}