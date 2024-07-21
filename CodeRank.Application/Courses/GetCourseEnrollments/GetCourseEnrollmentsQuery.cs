using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Courses;
using System;
using System.Collections.Generic;

namespace CodeRank.Application.Courses.GetCourseEnrollments;

public sealed record GetCourseEnrollmentsQuery(Guid CourseId) : IQuery<List<EnrollmentResponse>>;

public sealed class EnrollmentResponse
{
    public Guid Id { get; init; }
    public Guid StudentId { get; init; }
    public DateTime EnrollmentDate { get; init; }
}


 
public sealed class GetCourseEnrollmentsQueryHandler : IQueryHandler<GetCourseEnrollmentsQuery, List<EnrollmentResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseEnrollmentsQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<EnrollmentResponse>>> Handle(GetCourseEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            return Result.Failure<List<EnrollmentResponse>>(Error.CreateFormCallerName("GetCourseEnrollments", "Course not found"));
        }

        var enrollments = course.Enrollments.Select(e => new EnrollmentResponse
        {
            Id = e.Id,
            StudentId = e.StudentId,
            EnrollmentDate = e.EnrolmentDate
        }).ToList();

        return Result.Success(enrollments);
    }
}