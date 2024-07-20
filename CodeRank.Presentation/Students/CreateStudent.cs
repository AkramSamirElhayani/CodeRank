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
using CodeRank.Application.Students.CreateTeacher;
using CodeRank.Presentation.ApiResults;

namespace CodeRank.Presentation.Students;

public static class CreateStudent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Students", async (CreateStudentRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateStudentCommand(request.Name,request.Email));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
          
        .WithTags(Tags.Student);
    }

    internal sealed class CreateStudentRequest
    {
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
