using CodeRank.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace CodeRank.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? Guid.Parse(userId) : Guid.Empty;
        }
    }

    public bool IsInstructor => _httpContextAccessor.HttpContext?.User.IsInRole("Instructor") ?? false;
}