using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Courses;
using System.Collections.Generic;

 
namespace CodeRank.Application.Courses.GetCoursesByInstructor;

public sealed record GetCoursesByInstructorQuery() : IQuery<List<CourseListResponse>>;

public sealed class CourseListResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public DateTime StartDate { get; init; }
    public int EnrollmentCount { get; init; }
}
 

public sealed class GetCoursesByInstructorQueryHandler : IQueryHandler<GetCoursesByInstructorQuery, List<CourseListResponse>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCoursesByInstructorQueryHandler(
        ICourseRepository courseRepository,
        ICurrentUserService currentUserService)
    {
        _courseRepository = courseRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<CourseListResponse>>> Handle(GetCoursesByInstructorQuery request, CancellationToken cancellationToken)
    {
        var instructorId = _currentUserService.UserId;
        if (instructorId == Guid.Empty || !_currentUserService.IsInstructor)
        {
            return Result.Failure<List<CourseListResponse>>(Error.CreateFormCallerName("GetCoursesByInstructor", "User is not authenticated or is not an instructor."));
        }

        var courses = await _courseRepository.GetCoursesByInstructorIdAsync(instructorId, cancellationToken);

        var response = courses.Select(course => new CourseListResponse
        {
            Id = course.Id,
            Title = course.Title,
            StartDate = course.StartDate,
            EnrollmentCount = course.Enrollments.Count
        }).ToList();

        return Result.Success(response);
    }
}