using CodeRank.Application.Courses.GetCourseEnrollments;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace CodeRank.Presentation.Courses;

public static class GetCourseEnrollments
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("courses/{id}/enrollments", async (Guid id, ISender sender) =>
        {
            // TODO: Add authorization check here to ensure only instructors can access this endpoint
            Result<List<EnrollmentResponse>> result = await sender.Send(new GetCourseEnrollmentsQuery(id));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Course)
        .RequireAuthorization(); // Ensure this endpoint requires authentication
    }
}