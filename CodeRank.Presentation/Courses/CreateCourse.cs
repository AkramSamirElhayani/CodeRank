using CodeRank.Application.Courses.CreateCourse;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace CodeRank.Presentation.Courses;

public static class CreateCourse
{
    public static void MapEndpoint(IEndpointRouteBuilder apiGroup)
    {
        apiGroup.MapPost("courses", async (CreateCourseRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateCourseCommand(
                request.Title,
                request.StartDate));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Course)
        .RequireAuthorization("InstructorPolicy");
    }

    internal sealed class CreateCourseRequest
    {
        public string Title { get; init; }
        public DateTime StartDate { get; init; }
    }
}