using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.AdminCourses.DTOs;
using IGenServer.Domain.Entities;
using MediatR;

namespace IGenServer.Application.Features.AdminCourses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly IAdminCourseRepository _repository;

    public CreateCourseCommandHandler(IAdminCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var programExists = await _repository.ProgramExistsAsync(command.ProgramId, cancellationToken);
        if (!programExists)
        {
            throw new InvalidOperationException("Program does not exist.");
        }

        var title = command.Title.Trim();

        var courseExists = await _repository.CourseExistsAsync(
            command.ProgramId,
            title,
            cancellationToken);

        if (courseExists)
        {
            throw new InvalidOperationException("Course already exists in this program.");
        }

        var course = new Course
        {
            ProgramId = command.ProgramId,
            Title = title,
            Description = command.Description.Trim(),
            DurationMonths = command.DurationMonths,
            Price = command.Price,
            Status = "Draft"
        };

        await _repository.AddAsync(course, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new CourseDto(
            course.Id,
            course.ProgramId,
            course.Title,
            course.Description,
            course.DurationMonths,
            course.Price,
            course.Status,
            0);
    }
}