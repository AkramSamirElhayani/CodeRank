using CodeRank.Application.Courses.EnrollStudent;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace CodeRank.Presentation.Courses;

public static class EnrollStudent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("courses/{courseId}/enroll", async (Guid courseId, EnrollStudentRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new EnrollStudentCommand(
                request.StudentId,
                courseId));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Course);
    }

    internal sealed class EnrollStudentRequest
    {
        public Guid StudentId { get; init; }
    }
}