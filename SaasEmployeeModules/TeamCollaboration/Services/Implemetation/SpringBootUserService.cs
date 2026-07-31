using System.Net.Http.Headers;
using System.Text.Json;
using TeamCollaboration.DTOs.Responses;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Services.Implementation
{
    public class SpringBootUserService : ISpringBootUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SpringBootUserService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _httpClient.BaseAddress = new Uri(
                _configuration["SpringBoot:BaseUrl"]!);
        }

        public async Task<List<UserResponseDto>> GetCompanyUsersAsync(string jwtToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwtToken);

            var response =
                await _httpClient.GetAsync("/admin/users/company");

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserResponseDto>>(
                       json,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       })
                   ?? new List<UserResponseDto>();
        }

        public async Task<UserResponseDto> GetCurrentUserAsync(string jwtToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwtToken);

            var response =
                await _httpClient.GetAsync("/admin/users/me");

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserResponseDto>(
                       json,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       })!;
        }
    }
}