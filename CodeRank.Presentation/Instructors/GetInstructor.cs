using CodeRank.Application.Instructors.GetInstructor;
using CodeRank.Application.Teachers.GetInstructor;
using CodeRank.Domain.Abstractions;
using CodeRank.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Presentation.Instructors;
internal static class GetInstructor
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("instructors/{id}", async (Guid id, ISender sender) =>
        {
            Result<InstructorResponse> result = await sender.Send(new GetInstructorQuery(id));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Instructor);
    }
}

