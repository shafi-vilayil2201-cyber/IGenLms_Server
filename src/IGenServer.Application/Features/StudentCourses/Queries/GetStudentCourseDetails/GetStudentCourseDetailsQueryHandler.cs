using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentCourses.DTOs;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Queries.GetStudentCourseDetails;

public sealed class GetStudentCourseDetailsQueryHandler
    : IRequestHandler<GetStudentCourseDetailsQuery, StudentCourseDetailsDto>
{
    private readonly IStudentCourseRepository _repository;

    public GetStudentCourseDetailsQueryHandler(IStudentCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentCourseDetailsDto> Handle(
        GetStudentCourseDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var course = await _repository.GetCourseDetailsAsync(query.CourseId, cancellationToken);

        if (course is null)
        {
            throw new InvalidOperationException("Course does not exist.");
        }

        var isEnrolled = await _repository.IsEnrolledAsync(
            query.StudentUserId,
            query.CourseId,
            cancellationToken);

        return StudentCourseMapper.ToDetailsDto(course, isEnrolled);
    }
}