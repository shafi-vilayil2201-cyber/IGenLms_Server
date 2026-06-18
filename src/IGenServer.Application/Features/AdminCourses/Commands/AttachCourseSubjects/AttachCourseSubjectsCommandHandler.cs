using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminCourses.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Commands.AttachCourseSubjects;

public sealed class AttachCourseSubjectsCommandHandler
    : IRequestHandler<AttachCourseSubjectsCommand, CourseDetailsDto>
{
    private readonly IAdminCourseRepository _repository;

    public AttachCourseSubjectsCommandHandler(IAdminCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseDetailsDto> Handle(
        AttachCourseSubjectsCommand command,
        CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(command.CourseId, cancellationToken);
        if (course is null)
        {
            throw new InvalidOperationException("Course does not exist.");
        }

        foreach (var item in command.Subjects)
        {
            var subjectExists = await _repository.SubjectExistsInProgramAsync(
                item.SubjectId,
                course.ProgramId,
                cancellationToken);

            if (!subjectExists)
            {
                throw new InvalidOperationException("Subject does not exist in this course program.");
            }

            var alreadyAttached = await _repository.CourseSubjectExistsAsync(
                command.CourseId,
                item.SubjectId,
                cancellationToken);

            if (alreadyAttached)
            {
                continue;
            }

            await _repository.AddCourseSubjectAsync(new CourseSubject
            {
                CourseId = command.CourseId,
                SubjectId = item.SubjectId,
                DisplayOrder = item.DisplayOrder,
                StartMonth = item.StartMonth
            }, cancellationToken);
        }

        await _repository.SaveChangesAsync(cancellationToken);

        var updatedCourse = await _repository.GetByIdAsync(command.CourseId, cancellationToken)
            ?? throw new InvalidOperationException("Course does not exist.");

        return MapToDetails(updatedCourse);
    }

    private static CourseDetailsDto MapToDetails(Course course)
    {
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