using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Courses;

namespace CodeRank.Application.Courses.GetCourse;

public sealed class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, CourseResponse>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseResponse>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return Result.Failure<CourseResponse>(Error.CreateFormCallerName("GetCourse", "Course not found"));
        }

        return Result.Success(new CourseResponse
        {
            Id = course.Id,
            Title = course.Title,
            InstructorId = course.InstructorId,
            StartDate = course.StartDate,
            EnrollmentCount = course.Enrollments.Count
        });
    }
}