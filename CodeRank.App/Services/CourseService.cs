using System.Net.Http.Json;
using System.Text.Json;
using CodeRank.App.Handlers.Models;
using CodeRank.App.Models;

namespace CodeRank.App.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    public async Task<List<CourseListItem>> GetCoursesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/instructors/courses");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<CourseListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CourseListItem>();
            }
            else
            {
                // Handle error
                return new List<CourseListItem>();
            }
        }
        catch (Exception e)
        {
            // Log the exception
            return new List<CourseListItem>();
        }
    }

    public async Task<CourseDetails> GetCourseDetailsAsync(Guid courseId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/courses/{courseId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CourseDetails>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CourseDetails();
            }
            else
            {
                // Handle error
                return new CourseDetails();
            }
        }
        catch (Exception e)
        {
            // Log the exception
            return new CourseDetails();
        }
    }

    public async Task<Guid?> CreateCourseAsync(CreateCourseRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/courses", request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Guid>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                // Handle error
                return null;
            }
        }
        catch (Exception e)
        {
            // Log the exception
            return null;
        }
    }
}