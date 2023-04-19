using BettingTracker.Models.Dtos;
using BettingTracker.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;

namespace BettingTracker.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = http;
        _authStateProvider = authStateProvider;
    }

    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", request.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<string> GetUserEmail()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User.Identity.Name;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }

    public async Task<ServiceResponse<string>> Login(UserLogin request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

}
