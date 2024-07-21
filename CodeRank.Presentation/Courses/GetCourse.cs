using CodeRank.Application.Courses.GetCourse;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace CodeRank.Presentation.Courses;

public static class GetCourse
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("courses/{id}", async (Guid id, ISender sender) =>
        {
            Result<CourseResponse> result = await sender.Send(new GetCourseQuery(id));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Course);
    }
}