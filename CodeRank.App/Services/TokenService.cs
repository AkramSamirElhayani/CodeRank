﻿namespace CodeRank.App.Services;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class TokenService
{
    private readonly ProtectedLocalStorage _sessionStorage;

    public TokenService(ProtectedLocalStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task SetTokenAsync(string token)
    {
        await _sessionStorage.SetAsync("token", token);
    }

    public async Task<string> GetTokenAsync()
    {
        var result = await _sessionStorage.GetAsync<string>("token");
        return result.Success ? result.Value : null;
    }

    public async Task RemoveTokenAsync()
    {
        await _sessionStorage.DeleteAsync("token");
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