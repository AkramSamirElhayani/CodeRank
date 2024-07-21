namespace CodeRank.App.Services;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class TokenService
{
    private readonly ProtectedLocalStorage _sessionStorage;
    private string _cachedToken;
    public TokenService(ProtectedLocalStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task SetTokenAsync(string token)
    {
        await _sessionStorage.SetAsync("token", token);
        _cachedToken = token;
    }

    public async Task<string> GetTokenAsync()
    {
        if (_cachedToken == null)
        {
            var result = await _sessionStorage.GetAsync<string>("token");
            _cachedToken = result.Success ? result.Value : null;
        }
        return _cachedToken;
    }

    public async Task RemoveTokenAsync()
    {
        await _sessionStorage.DeleteAsync("token");
        _cachedToken = null;
    }

    // Similar methods for refresh token
    public async Task SetRefreshTokenAsync(string refreshToken)
    {
        await _sessionStorage.SetAsync("refreshToken", refreshToken);
    }

    public async Task<string> GetRefreshTokenAsync()
    {
        var result = await _sessionStorage.GetAsync<string>("refreshToken");
        return result.Success ? result.Value : null;
    }

    public async Task RemoveRefreshTokenAsync()
    {
        await _sessionStorage.DeleteAsync("refreshToken");
    }
}