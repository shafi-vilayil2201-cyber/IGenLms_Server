using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.StudentCourses.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.StudentCourses.Commands.EnrollInCourse;

public sealed class EnrollInCourseCommandHandler
    : IRequestHandler<EnrollInCourseCommand, StudentCourseDetailsDto>
{
    private readonly IStudentCourseRepository _repository;

    public EnrollInCourseCommandHandler(IStudentCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentCourseDetailsDto> Handle(
        EnrollInCourseCommand command,
        CancellationToken cancellationToken)
    {
        var course = await _repository.GetCourseDetailsAsync(command.CourseId, cancellationToken);

        if (course is null)
        {
            throw new InvalidOperationException("Course does not exist.");
        }

        var alreadyEnrolled = await _repository.IsEnrolledAsync(
            command.StudentUserId,
            command.CourseId,
            cancellationToken);

        if (!alreadyEnrolled)
        {
            await _repository.AddEnrollmentAsync(new StudentCourseEnrollment
            {
                StudentUserId = command.StudentUserId,
                CourseId = command.CourseId,
                EnrolledAtUtc = DateTime.UtcNow
            }, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);
        }

        return StudentCourseMapper.ToDetailsDto(course, true);
    }
}