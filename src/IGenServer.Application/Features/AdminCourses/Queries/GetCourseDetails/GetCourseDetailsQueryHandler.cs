using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Queries.GetCourseDetails;

public sealed class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailsDto>
{
    private readonly IAdminCourseRepository _repository;

    public GetCourseDetailsQueryHandler(IAdminCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseDetailsDto> Handle(GetCourseDetailsQuery query, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(query.CourseId, cancellationToken);
        if (course is null)
        {
            throw new InvalidOperationException("Course does not exist.");
        }

        return new CourseDetailsDto(
            course.Id,
            course.ProgramId,
            course.Title,
            course.Description,
            course.DurationMonths,
            course.Price,
            course.Status,
            course.CourseSubjects
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new CourseSubjectDto(
                    x.SubjectId,
                    x.Subject.Name,
                    x.DisplayOrder,
                    x.StartMonth))
                .ToList());
    }
}