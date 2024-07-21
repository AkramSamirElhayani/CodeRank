namespace CodeRank.App.Identity;
 
using System.Net.Http.Json;
using System.Text.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Identity");
    }

    public async Task<AuthResponse> Login(LoginModel loginModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Identity/login", loginModel);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<AuthResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                // Try to deserialize error response
                var errorResponse = JsonSerializer.Deserialize<AuthResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return errorResponse;
            }
        }
        catch (Exception e)
        {
            // Log the exception here if you have logging set up
            return new AuthResponse { IsAuthSuccessful = false, ErrorMessage = "An unexpected error occurred. Please try again." };
        }
    }

    public async Task<RegistrationResponse> Register(RegisterModel registerModel)
    {
        var response = await _httpClient.PostAsJsonAsync("Identity/register", registerModel);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RegistrationResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

}
public class ErrorResponse
{
    public string ErrorMessage { get; set; }
}