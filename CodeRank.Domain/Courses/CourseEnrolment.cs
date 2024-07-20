namespace CodeRank.Domain.Courses;
using System;
using CodeRank.Domain.Abstractions;

public class CourseEnrolment:IEntity
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrolmentDate { get; private set; }

    private CourseEnrolment(Guid studentId, Guid courseId, DateTime enrolmentDate)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        CourseId = courseId;
        EnrolmentDate = enrolmentDate;
    }

    internal static Result<CourseEnrolment> Create(Guid studentId, Guid courseId, DateTime enrolmentDate)
    {
        return Result.Success(new CourseEnrolment(studentId, courseId, enrolmentDate));
    }
}
