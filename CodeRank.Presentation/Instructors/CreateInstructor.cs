using MediatR;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using CodeRank.Domain.Abstractions;
using CodeRank.Application.Instructors.CreateTeacher;
using CodeRank.Presentation.ApiResults;

namespace CodeRank.Presentation.Instructors;

public static class CreateInstructor
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("instructors", async (CreateInstructorRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateInstructorCommand(request.Name,request.Email));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Instructor);
    }

    internal sealed class CreateInstructorRequest
    {
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
