using IGenServer.Application.Features.StudentCourses.DTOs;
using IGenServer.Domain.Entities;

namespace IGenServer.Application.Features.StudentCourses;

internal static class StudentCourseMapper
{
    public static StudentCourseDto ToListDto(Course course, bool isEnrolled)
    {
        return new StudentCourseDto(
            course.Id,
            course.ProgramId,
            course.Title,
            course.Description,
            course.DurationMonths,
            course.Price,
            course.Status,
            course.CourseSubjects.Count,
            isEnrolled);
    }

    public static StudentCourseDetailsDto ToDetailsDto(Course course, bool isEnrolled)
    {
        return new StudentCourseDetailsDto(
            course.Id,
            course.ProgramId,
            course.Title,
            course.Description,
            course.DurationMonths,
            course.Price,
            course.Status,
            isEnrolled,
            course.CourseSubjects
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new StudentCourseSubjectDto(
                    x.SubjectId,
                    x.Subject.Name,
                    x.DisplayOrder,
                    x.StartMonth))
                .ToList());
    }
}