using CodeRank.Application.Students.GetStudent;
using CodeRank.Application.Students.GetStudent;
using CodeRank.Application.Teachers.GetStudent;
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

namespace CodeRank.Presentation.Students;
internal static class GetStudent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("students/{id}", async (Guid id, ISender sender) =>
        {
            Result<StudentResponse> result = await sender.Send(new GetStudentQuery(id));

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Student);
    }
}

