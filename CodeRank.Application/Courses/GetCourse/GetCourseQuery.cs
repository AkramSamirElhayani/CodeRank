using CodeRank.Application.Abstractions;
using System;

namespace CodeRank.Application.Courses.GetCourse;

public sealed record GetCourseQuery(Guid Id) : IQuery<CourseResponse>;


 
 

public sealed class CourseResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public Guid? InstructorId { get; init; }
    public DateTime StartDate { get; init; }
    public int EnrollmentCount { get; init; }
}
