namespace CodeRank.App.Models;

public class CourseListItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid TopicId { get; set; }
    public DateTime StartDate { get; set; }
    public int EnrollmentCount { get; set; }
}

public class CourseDetails : CourseListItem
{
    public List<StudentEnrollment> Enrollments { get; set; } = new List<StudentEnrollment>();
}

public class StudentEnrollment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
}

public class CreateCourseRequest
{
    public string Title { get; set; } = string.Empty;
    public Guid TopicId { get; set; }
    public DateTime StartDate { get; set; }
}