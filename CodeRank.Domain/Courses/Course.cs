namespace CodeRank.Domain.Courses;
using System;
using System.Collections.Generic;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain;

public class Course : IEntity
{
 
    private List<CourseEnrolment> _enrollments = new List<CourseEnrolment>();


    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Guid? InstructorId { get; private set; }
    public DateTime StartDate { get; private set; }
    public IReadOnlyCollection<CourseEnrolment> Enrollments => _enrollments.AsReadOnly();
 

    private Course(
        Guid id,
        string title,
        Guid? instructorId,
        DateTime startDate)
    {
        Id = id;
        Title = title;
        StartDate = startDate;
        InstructorId = instructorId;
    }

    public static Result<Course> Create(
        string title,
        Guid instructorId,
        DateTime startDate)
    {
        var result = ValidateTitle(title);
        if (result.IsFailure) return Result.Failure<Course>(result.Errors);
        return Result.Success(new Course(Guid.NewGuid(), title, instructorId,  startDate));
    }

    public Result SetTitle(string title)
    {
        var result = ValidateTitle(title);
        if (result.IsFailure) return result;

        Title = title;
        return Result.Success();
    }

    public Result AssignInstructor(Guid instructorId)
    {
        InstructorId = instructorId;
        return Result.Success();
    }

    public Result UnassignInstructor()
    {
        InstructorId = null;
        return Result.Success();
    }  
    public Result EnrollStudent(CourseEnrolment enrolment)
    {
        _enrollments.Add(enrolment);
        return Result.Success();
    }
    private static Result ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure(Error.CreateFormCallerName("Course", "Course title cannot be empty or whitespace."));
        return Result.Success();
    }
}
