
using CodeRank.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CodeRank.Presentation.ApiResults;

public static class ApiResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Microsoft.AspNetCore.Http.Results.Problem(
            title: result.Error.Code,
            detail: result.Error.Message);

      
      
    }
}
