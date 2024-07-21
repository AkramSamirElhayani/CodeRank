using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CodeRank.Presentation.Courses;

public static class CourseEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateCourse.MapEndpoint(app);
        EnrollStudent.MapEndpoint(app);
        GetCourse.MapEndpoint(app);
        GetCoursesByInstructor.MapEndpoint(app);
        GetCourseEnrollments.MapEndpoint(app);
    }
}