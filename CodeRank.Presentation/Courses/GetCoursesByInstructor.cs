using CodeRank.Application.Courses.GetCoursesByInstructor;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CodeRank.Presentation.Courses;

public static class GetCoursesByInstructor
{
    public static void MapEndpoint(IEndpointRouteBuilder apiGroup)
    {
        apiGroup.MapGet("instructors/courses", async (ISender sender) =>
        {
            Result<List<CourseListResponse>> result = await sender.Send(new GetCoursesByInstructorQuery());

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Course)
        .RequireAuthorization("InstructorPolicy");
    }
}