namespace CodeRank.API.Identity;

using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class RBACMiddleware
{
    private readonly RequestDelegate _next;

    public RBACMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            // Extract actor from JWT claims
            var actor = context.User.FindFirstValue(ClaimTypes.Actor);

            // Perform dynamic RBAC logic based on actor
            // Example: Check if the user has access to the requested resource

            if (!HasAccessToResource(actor, context.Request.Path, context.Request.Method))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }
        await _next(context);
    }

    private bool HasAccessToResource(string actor, string resourcePath, string method)
    {
        // Implement your RBAC logic here
        // Compare the actor with the resourcePath to determine access
        // Return true if user has access, otherwise false

        var controller = ExtractControllerNameFromUrl(resourcePath);

        return true; // Replace with your actual logic
    }


    public static string ExtractControllerNameFromUrl(string url)
    {
        string pattern = @"api/([^/]+)";

        Match match = Regex.Match(url, pattern);

        if (match.Success && match.Groups.Count > 1)
        {
            return match.Groups[1].Value;
        }
        return null;
    }
}
