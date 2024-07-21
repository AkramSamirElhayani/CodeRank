using CodeRank.App.Services;
using System.Net.Http.Headers;

namespace CodeRank.App.Handlers.Models;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly TokenService _tokenService;

    public AuthenticationDelegatingHandler(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}